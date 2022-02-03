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
        public Handler(DataContext context, IMapper mapper, IOriginAccessor originAccessor)
        {
            _originAccessor = originAccessor;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<CityPlaceDtoQuery>> Handle(Query request, CancellationToken cancellationToken)
        {
            var cityPlace = await _context.CityPlace
                    .Include(x => x.Schedules)
                    .ProjectTo<CityPlaceDtoQuery>(_mapper.ConfigurationProvider, new { currentOrigin = _originAccessor.GetOrigin() })
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.CityId == request.CityId, cancellationToken);

            if (cityPlace == null) return Result<CityPlaceDtoQuery>.Failure("City place not found");

            cityPlace.Schedules = cityPlace.Schedules.OrderBy(x => x.DayWeek).ToList();

            return Result<CityPlaceDtoQuery>.Success(cityPlace);
        }
    }
}
