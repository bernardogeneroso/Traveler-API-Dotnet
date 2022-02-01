using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.Cities.DTOs;
using Services.Interfaces;

namespace Services.Cities;

public class List
{
    public class Query : IRequest<Result<List<CityDtoQuery>>>
    {
    }

    public class Handler : IRequestHandler<Query, Result<List<CityDtoQuery>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IOriginAccessor _originAccessor;

        public Handler(DataContext context, IMapper mapper, IOriginAccessor originAccessor)
        {
            _mapper = mapper;
            _originAccessor = originAccessor;
            _context = context;
        }

        public async Task<Result<List<CityDtoQuery>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var cities = await _context.Cities
                    .AsNoTracking()
                    .ProjectTo<CityDtoQuery>(_mapper.ConfigurationProvider, new { currentOrigin = _originAccessor.GetOrigin() })
                    .ToListAsync();

            return Result<List<CityDtoQuery>>.Success(cities);
        }
    }
}
