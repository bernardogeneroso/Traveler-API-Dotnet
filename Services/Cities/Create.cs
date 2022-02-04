using Application.Core;
using AutoMapper;
using Database;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.Cities.DTOs;
using Services.Interfaces;

namespace Services.Cities;

public class Create
{
    public class Command : IRequest<Result<Unit>>
    {
        public CityDtoCreateRequest City { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.City).SetValidator(new CityCreateValidator()).NotEmpty();
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
                    .AsNoTracking()
                    .Where(x => x.Name == request.City.Name)
                    .FirstOrDefaultAsync(cancellationToken);

            if (city != null) return Result<Unit>.Failure("City already exists");

            var newCity = _mapper.Map(request.City, city);

            var uploadResult = await _imageAccessor.AddImage(request.City.File, cancellationToken);

            newCity.ImageName = uploadResult.Filename;
            newCity.ImagePublicId = uploadResult.PublicId;

            _context.City.Add(newCity);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failed to create city");

            return Result<Unit>.SuccessNoContent(Unit.Value);
        }
    }
}
