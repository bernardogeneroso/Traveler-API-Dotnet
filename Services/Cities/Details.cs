using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.Cities.DTOs;
using Services.CitiesCategories.DTOs;
using Services.Interfaces;

namespace Services.Cities;

public class Details
{
    public class Query : IRequest<Result<CityDtoDetailQuery>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<CityDtoDetailQuery>>
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

        public async Task<Result<CityDtoDetailQuery>> Handle(Query request, CancellationToken cancellationToken)
        {
            var cityDto = await _context.Cities
                        .Include(x => x.Detail)
                        .AsNoTracking()
                        .ProjectTo<CityDtoQuery>(_mapper.ConfigurationProvider, new { currentOrigin = _originAccessor.GetOrigin() })
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (cityDto == null) return Result<CityDtoDetailQuery>.Failure("City not found");

            var categoriesDto = await _context.CategoriesCities
                        .AsNoTracking()
                        .ProjectTo<CategoryCityDtoQuery>(_mapper.ConfigurationProvider, new { currentOrigin = _originAccessor.GetOrigin() })
                        .ToListAsync(cancellationToken);

            var cityDtoDetail = new CityDtoDetailQuery
            {
                City = cityDto,
                Categories = categoriesDto
            };

            return Result<CityDtoDetailQuery>.Success(cityDtoDetail);
        }
    }
}
