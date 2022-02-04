using Application.Core;
using AutoMapper;
using Database;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.CitiesCategories.DTOs;
using Services.Interfaces;

namespace Services.CitiesCategories;

public class Create
{
    public class Command : IRequest<Result<Unit>>
    {
        public CategoryCityDtoRequest Category { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Category).SetValidator(new CategoryCityValidator()).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IImageAccessor _imageAccessor;
        public Handler(DataContext context, IMapper mapper, IImageAccessor imageAccessor)
        {
            _imageAccessor = imageAccessor;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var categories = await _context.CategoryCity
                    .AsNoTracking()
                    .Select(x => x.Name)
                    .ToListAsync(cancellationToken);

            if (categories.Count() == 3) return Result<Unit>.Failure("Cannot add more than 3 categories");

            var existCategory = categories.Any(name => name == request.Category.Name);

            if (existCategory) return Result<Unit>.Failure("Category already exist");

            var category = _mapper.Map<CategoryCity>(request.Category);

            var uploadResult = await _imageAccessor.AddImage(request.Category.File);

            if (uploadResult == null) return Result<Unit>.Failure("Failed to upload image");

            category.ImageName = uploadResult.Filename;
            category.ImagePublicId = uploadResult.PublicId;

            _context.CategoryCity.Add(category);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failed to create category");

            return Result<Unit>.SuccessNoContent(Unit.Value);
        }
    }
}
