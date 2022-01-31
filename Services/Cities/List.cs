using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.Cities;

public class List
{
    public class Query : IRequest<Result<List<CityDto>>>
    {
    }

    public class Handler : IRequestHandler<Query, Result<List<CityDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<List<CityDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var cities = await _context.Cities
                    .AsNoTracking()
                    .ProjectTo<CityDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

            return Result<List<CityDto>>.Success(cities);
        }
    }
}
