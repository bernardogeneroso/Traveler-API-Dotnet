using Application.Core;
using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.CitiesPlaces;

public class SetHighlighted
{
    public class Command : IRequest<Result<Unit>>
    {
        public Guid CityId { get; set; }
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
            var place = await _context.CityPlace.FirstOrDefaultAsync(x => x.Id == request.Id && x.CityId == request.CityId, cancellationToken);

            if (place == null) return Result<Unit>.Failure("Failed to set highlighted place");

            var oldPlaceHighlighted = await _context.CityPlace.Select(x => new { x.Id, x.CityId, x.IsHighlighted }).FirstOrDefaultAsync(x => x.CityId == request.CityId && x.IsHighlighted, cancellationToken);

            if (oldPlaceHighlighted != null)
            {
                var oldPlace = new CityPlace
                {
                    Id = oldPlaceHighlighted.Id,
                    CityId = oldPlaceHighlighted.CityId,
                    IsHighlighted = false
                };

                _context.CityPlace.Attach(oldPlace);

                _context.Entry(oldPlace).Property(x => x.IsHighlighted).IsModified = true;
            }

            place.IsHighlighted = true;

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failed to set highlighted place");

            return Result<Unit>.SuccessNoContent(Unit.Value);
        }
    }
}
