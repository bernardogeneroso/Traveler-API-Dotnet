using Application.Core;
using AutoMapper;
using Database;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.CitiesPlaces.DTOs;

namespace Services.CitiesPlaces;

public class Create
{
    public class Command : IRequest<Result<Unit>>
    {
        public Guid CityId { get; set; }
        public Guid CategoryId { get; set; }
        public CityPlaceDtoRequest Place { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Place).SetValidator(new CityPlaceValidator()).NotEmpty();
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
            var category = await _context.CategoryCity.FindAsync(request.CategoryId);

            if (category == null) return Result<Unit>.Failure("Category does not exist");

            if (!await _context.City.AsNoTracking().AnyAsync(x => x.Id == request.CityId, cancellationToken)) return Result<Unit>.Failure("City does not exist");

            var existCityPlace = await _context.CityPlace
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.CityId == request.CityId
                        && x.CategoryId == request.CategoryId
                        && x.Name == request.Place.Name, cancellationToken
                    );

            if (existCityPlace != null) return Result<Unit>.Failure("Place already exist");

            category.Places += 1;

            var cityPlace = _mapper.Map<CityPlace>(request.Place);

            _context.CityPlace.Add(cityPlace);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failed creating place");

            return Result<Unit>.SuccessNoContent(Unit.Value);
        }
    }
}
