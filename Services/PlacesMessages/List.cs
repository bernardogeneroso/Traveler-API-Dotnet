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
        public Handler(DataContext context, IMapper mapper, IOriginAccessor originAccessor)
        {
            _originAccessor = originAccessor;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<List<CityPlaceMessageDtoQuery>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var cityPlaceMessage = await _context.CityPlaceMessage
                .ProjectTo<CityPlaceMessageDtoQuery>(_mapper.ConfigurationProvider, new { currentOrigin = _originAccessor.GetOrigin() })
                .Where(x => x.PlaceId == request.PlaceId)
                .ToListAsync(cancellationToken);

            return Result<List<CityPlaceMessageDtoQuery>>.Success(cityPlaceMessage);
        }
    }
}
