using Application.Core;
using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.CitiesPlacesSchedules;

public class Delete
{
    public class Command : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
        public Guid PlaceId { get; set; }
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
            var existSchedule = await _context.CityPlaceSchedule
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.PlaceId == request.PlaceId, cancellationToken);

            if (existSchedule == null) return Result<Unit>.Failure("Schedule not found");

            _context.CityPlaceSchedule.Remove(existSchedule);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Fail to delete schedule");

            return Result<Unit>.SuccessNoContent(Unit.Value);
        }
    }
}
