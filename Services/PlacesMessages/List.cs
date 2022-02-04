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
    public class Query : IRequest<Result<CityPlaceMessageDtoQuery>>
    {
        public Guid PlaceId { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<CityPlaceMessageDtoQuery>>
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

        public async Task<Result<CityPlaceMessageDtoQuery>> Handle(Query request, CancellationToken cancellationToken)
        {

            var cityPlaceMessage = await _context.CityPlaceMessage
                .ProjectTo<CityPlaceMessageDtoQuery>(_mapper.ConfigurationProvider, new { currentOrigin = _originAccessor.GetOrigin() })
                .FirstOrDefaultAsync(x => x.PlaceId == request.PlaceId, cancellationToken);

            return Result<CityPlaceMessageDtoQuery>.Success(cityPlaceMessage);
        }
    }
}
