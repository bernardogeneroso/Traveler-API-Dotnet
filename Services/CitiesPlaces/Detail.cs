using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.CitiesPlaces.DTOs;
using Services.Interfaces;

namespace Services.CitiesPlaces;

public class Detail
{
    public class Query : IRequest<Result<CityPlaceDtoQuery>>
    {
        public Guid CityId { get; set; }
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<CityPlaceDtoQuery>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IOriginAccessor _originAccessor;
        private readonly IRedisCacheAccessor _redisCacheAccessor;
        public Handler(DataContext context, IMapper mapper, IOriginAccessor originAccessor, IRedisCacheAccessor redisCacheAccessor)
        {
            _redisCacheAccessor = redisCacheAccessor;
            _originAccessor = originAccessor;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<CityPlaceDtoQuery>> Handle(Query request, CancellationToken cancellationToken)
        {
            var keyMaster = new string[] { "detail" };

            var cityPlaceDtoDetailCached = await _redisCacheAccessor.GetCacheValueAsync<CityPlaceDtoQuery>(keyMaster);

            if (cityPlaceDtoDetailCached != null) return Result<CityPlaceDtoQuery>.Success(cityPlaceDtoDetailCached);

            var place = await _context.CityPlace
                    .AsNoTracking()
                    .Include(x => x.Schedules)
                    .ProjectTo<CityPlaceDtoQuery>(_mapper.ConfigurationProvider, new { currentUrlCloudinary = _originAccessor.GetCloudinaryUrl() })
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.CityId == request.CityId, cancellationToken);

            if (place == null) return Result<CityPlaceDtoQuery>.Failure("City place not found");

            place.Schedules = place.Schedules
                    .OrderBy(x => x.DayWeek)
                    .ToList();

            await _redisCacheAccessor.SetCacheValueAsync(place, keyMaster);

            return Result<CityPlaceDtoQuery>.Success(place);
        }
    }
}
