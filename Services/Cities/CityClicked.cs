using Application.Core;
using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Cities;

public class CityClicked
{
    public class Command : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
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
            var cityDb = await _context.City
                    .AsNoTracking()
                    .Select(x => new { x.Id, x.ClickedCount })
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (cityDb == null) return Result<Unit>.Failure("Couldn't add a click to the city");

            var city = new City
            {
                Id = request.Id
            };

            city.ClickedCount = cityDb.ClickedCount + 1;

            _context.Entry(city).Property(x => x.ClickedCount).IsModified = true;

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Couldn't add a click to the city");

            return Result<Unit>.SuccessNoContent(Unit.Value);
        }
    }
}
