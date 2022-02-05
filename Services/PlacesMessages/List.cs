using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.PlacesMessages.DTOs;

namespace Services.PlacesMessages;

public class List
{
    public class Query : IRequest<Result<List<CityPlaceMessageDtoQuery>>>
    {
        public Guid PlaceId { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<List<CityPlaceMessageDtoQuery>>>
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

        public async Task<Result<List<CityPlaceMessageDtoQuery>>> Handle(Query request, CancellationToken cancellationToken)
        {
            string[] keyMaster = { "list", request.PlaceId.ToString() };

            var cityPlaceMessageDtoListCached = await _redisCacheAccessor.GetCacheValueAsync<List<CityPlaceMessageDtoQuery>>(keyMaster);

            if (cityPlaceMessageDtoListCached != null) return Result<List<CityPlaceMessageDtoQuery>>.Success(cityPlaceMessageDtoListCached);

            var cityPlaceMessage = await _context.CityPlaceMessage
                .AsNoTracking()
                .Where(x => x.PlaceId == request.PlaceId)
                .ProjectTo<CityPlaceMessageDtoQuery>(_mapper.ConfigurationProvider, new { currentUrlCloudinary = _originAccessor.GetCloudinaryUrl() })
                .ToListAsync(cancellationToken);

            await _redisCacheAccessor.SetCacheValueAsync(keyMaster, cityPlaceMessage);

            return Result<List<CityPlaceMessageDtoQuery>>.Success(cityPlaceMessage);
        }
    }
}
