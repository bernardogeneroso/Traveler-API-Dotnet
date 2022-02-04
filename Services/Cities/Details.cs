using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.Cities.DTOs;
using Services.CitiesCategories.DTOs;
using Services.CitiesPlaces.DTOs;

namespace Services.Cities;

public class Details
{
    public class Query : IRequest<Result<CityDtoGetQuery>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<CityDtoGetQuery>>
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

        public async Task<Result<CityDtoGetQuery>> Handle(Query request, CancellationToken cancellationToken)
        {
            var urlCloudinary = _config.GetSection("Cloudinary").GetValue<string>("Url");

            var cityDto = await _context.City
                        .Include(x => x.Detail)
                        .AsNoTracking()
                        .ProjectTo<CityDtoDetailsQuery>(_mapper.ConfigurationProvider, new { currentUrlCloudinary = urlCloudinary })
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (cityDto == null) return Result<CityDtoGetQuery>.Failure("City not found");

            var categoriesDto = await _context.CategoryCity
                        .AsNoTracking()
                        .ProjectTo<CategoryCityDtoQuery>(_mapper.ConfigurationProvider, new { currentUrlCloudinary = urlCloudinary })
                        .ToListAsync(cancellationToken);

            var placeHighlighted = await _context.CityPlace
                        .AsNoTracking()
                        .ProjectTo<CityPlaceDtoHighlightQuery>(_mapper.ConfigurationProvider, new { currentUrlCloudinary = urlCloudinary })
                        .FirstOrDefaultAsync(x => x.CityId == cityDto.Id && x.IsHighlighted, cancellationToken);

            var cityDtoDetail = new CityDtoGetQuery
            {
                City = cityDto,
                PlaceHighlighted = placeHighlighted,
                Categories = categoriesDto
            };

            return Result<CityDtoGetQuery>.Success(cityDtoDetail);
        }
    }
}
