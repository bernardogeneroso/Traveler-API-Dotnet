using Application.Core;
using Database;
using MediatR;

namespace Services.CitiesPlaces;

public class Detail
{
    public class Query : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<Unit>>
    {
        private readonly DataContext _context;
        public Handler(DataContext context)
        {
            _context = context;
        }

        public Task<Result<Unit>> Handle(Query request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
