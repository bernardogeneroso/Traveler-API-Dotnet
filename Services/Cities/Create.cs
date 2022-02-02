using Application.Core;
using AutoMapper;
using Database;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.Cities.DTOs;

namespace Services.Cities;

public class Create
{
    public class Command : IRequest<Result<Unit>>
    {
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
        public Handler(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var city = await _context.Cities
                    .Where(x => x.Name == request.City.Name)
                    .FirstOrDefaultAsync();

            if (city != null) return Result<Unit>.Failure("City already exists");

            var newCity = _mapper.Map(request.City, city);

            _context.Cities.Add(newCity);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failed to create city");

            return Result<Unit>.SuccessNoContent(Unit.Value);
        }
    }
}
