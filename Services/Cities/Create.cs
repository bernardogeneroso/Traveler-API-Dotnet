using Application.Core;
using Database;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Cities;

public class Create
{
    public class Command : IRequest<Result<Unit>>
    {
        public City City { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.City).SetValidator(new CityValidator());
        }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;
        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var city = await _context.Cities
                    .Where(x => x.Name == request.City.Name)
                    .FirstOrDefaultAsync();

            if (city != null) return Result<Unit>.Failure("City already exists");

            _context.Cities.Add(request.City);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failed to create city");

            return Result<Unit>.SuccessNoContent(Unit.Value);
        }
    }
}
