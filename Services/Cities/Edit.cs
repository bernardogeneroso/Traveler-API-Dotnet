using Application.Core;
using AutoMapper;
using Database;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.Cities.DTOs;
using Services.Interfaces;

namespace Services.Cities;

public class Edit
{
    public class Command : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
        public CityDtoRequest City { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.City).SetValidator(new CityValidator()).NotEmpty();
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
            var city = await _context.City
                    .Include(x => x.Detail)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (city == null) return Result<Unit>.Failure("Couldn't find the city");

            if (request.City.File != null && request.City.File.Length > 0)
            {
                if (city.ImagePublicId != null)
                {
                    var resultDeleteImageAsync = await _imageAccessor.DeleteImageAsync(city.ImagePublicId);

                    if (resultDeleteImageAsync == null) return Result<Unit>.Failure("Failed to delete image");
                }

                var uploadResult = await _imageAccessor.AddImageAsync(request.City.File, cancellationToken);

                city.ImageName = uploadResult.Filename;
                city.ImagePublicId = uploadResult.PublicId;
            }

            _mapper.Map(request.City, city);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Couldn't update the city");

            return Result<Unit>.SuccessNoContent(Unit.Value);
        }
    }
}
