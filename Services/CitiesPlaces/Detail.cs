using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _config;
        public Handler(DataContext context, IMapper mapper, IConfiguration config)
        {
            _config = config;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<CityPlaceDtoQuery>> Handle(Query request, CancellationToken cancellationToken)
        {
            var place = await _context.CityPlace
                    .AsNoTracking()
                    .Include(x => x.Schedules)
                    .ProjectTo<CityPlaceDtoQuery>(_mapper.ConfigurationProvider, new { currentOrigin = _config.GetSection("Cloudinary").GetValue<string>("Url") })
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.CityId == request.CityId, cancellationToken);

            if (place == null) return Result<CityPlaceDtoQuery>.Failure("City place not found");

            place.Schedules = place.Schedules
                    .OrderBy(x => x.DayWeek)
                    .ToList();

            return Result<CityPlaceDtoQuery>.Success(place);
        }
    }
}
