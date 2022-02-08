using Application.Core;
using Database;
using MediatR;
using Services.Interfaces;

namespace Services.CitiesCategories;

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
            var category = await _context.CategoryCity.FindAsync(new object[] { request.Id }, cancellationToken);

            if (category == null) return Result<Unit>.Failure("Category not found");

            if (category.ImagePublicId != null)
            {
                await _imageAccessor.DeleteImageAsync(category.ImagePublicId);
            }

            _context.CategoryCity.Remove(category);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failed to delete category");

            return Result<Unit>.SuccessNoContent(Unit.Value);
        }
    }
}
