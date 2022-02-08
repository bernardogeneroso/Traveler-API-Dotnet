using Application.Core;
using Database;
using MediatR;
using Services.Interfaces;

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
        private readonly IImageAccessor _imageAccessor;
        public Handler(DataContext context, IImageAccessor imageAccessor)
        {
            _imageAccessor = imageAccessor;
            _context = context;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var existCityPlace = await _context.CityPlace.FindAsync(new object[] { request.Id }, cancellationToken);

            if (existCityPlace == null) return Result<Unit>.Failure("Failed to delete the place");

            var category = await _context.CategoryCity.FindAsync(new object[] { existCityPlace.CategoryId }, cancellationToken);

            if (category == null) return Result<Unit>.Failure("Failed to delete the place");

            category.Places -= 1;

            if (existCityPlace.ImagePublicId != null)
            {
                await _imageAccessor.DeleteImageAsync(existCityPlace.ImagePublicId);
            }

            _context.CityPlace.Remove(existCityPlace);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failed to delete the place");

            return Result<Unit>.SuccessNoContent(Unit.Value);
        }
    }
}
