using Application.Core;
using AutoMapper;
using Database;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.CitiesCategories.DTOs;

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
        public Handler(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var categories = await _context.CategoryCity.Select(x => x.Name).ToListAsync(cancellationToken);

            if (categories.Count() == 3) return Result<Unit>.Failure("Cannot add more than 3 categories");

            var existCategory = categories.Any(name => name == request.Category.Name);

            if (existCategory) return Result<Unit>.Failure("Category already exist");

            var category = _mapper.Map<CategoryCity>(request.Category);

            _context.CategoryCity.Add(category);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failed to create category");

            return Result<Unit>.SuccessNoContent(Unit.Value);
        }
    }
}
