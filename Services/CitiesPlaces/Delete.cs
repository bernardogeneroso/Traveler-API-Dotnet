using Application.Core;
using Database;
using MediatR;

namespace Services.CitiesPlaces;

public class Delete
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
            var existCityPlace = await _context.CitiesPlaces
                    .FindAsync(request.Id);

            if (existCityPlace == null) return Result<Unit>.Failure("Failed to delete the place");

            var category = await _context.CategoriesCities.FindAsync(existCityPlace.CategoryId);

            if (category == null) return Result<Unit>.Failure("Failed to delete the place");

            category.Places -= 1;

            _context.CitiesPlaces.Remove(existCityPlace);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return Result<Unit>.Failure("Failed to delete the place");

            return Result<Unit>.SuccessNoContent(Unit.Value);
        }
    }
}
