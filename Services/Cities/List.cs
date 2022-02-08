using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Database;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.Cities.DTOs;
using Services.Interfaces;

namespace Services.Cities;

public class List
{
    public class Query : IRequest<Result<List<CityDtoQuery>>>
    {
        public int Filter { get; set; }
        public string Search { get; set; }
    }

    public class QueryValidator : AbstractValidator<Query>
    {
        public QueryValidator()
        {
            RuleFor(x => x.Filter).InclusiveBetween(0, 2);
        }
    }

    public class Handler : IRequestHandler<Query, Result<List<CityDtoQuery>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IOriginAccessor _originAccessor;
        private readonly IRedisCacheAccessor _redisCacheAccessor;

        public Handler(DataContext context, IMapper mapper, IRedisCacheAccessor redisCacheAccessor, IOriginAccessor originAccessor)
        {
            _redisCacheAccessor = redisCacheAccessor;
            _originAccessor = originAccessor;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<List<CityDtoQuery>>> Handle(Query request, CancellationToken cancellationToken)
        {
            // Manual validation using FluentValidation
            var validator = new QueryValidator();
            var resultValidation = await validator.ValidateAsync(request, cancellationToken);

            if (!resultValidation.IsValid) return Result<List<CityDtoQuery>>.Failure("Failed to get the cities", resultValidation);

            var query = _context.City
                    .AsNoTracking()
                    .AsQueryable();

            if (request.Filter == 1)
            {
                query = query.OrderByDescending(x => x.ClickedCount);
            }
            else if (request.Filter == 2)
            {
                query = query.OrderBy(x => x.Name);
            }

            var urlCloudinary = _originAccessor.GetCloudinaryUrl();

            var keyMaster = new string[] { request.Search, request.Filter.ToString() };

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var citiesDtoSearch = await _redisCacheAccessor
                                .GetCacheValueAsync<List<CityDtoQuery>>(keyMaster);

                if (citiesDtoSearch == null)
                {
                    citiesDtoSearch = await query
                        .AsNoTracking()
                        .ProjectTo<CityDtoQuery>(_mapper.ConfigurationProvider, new { currentUrlCloudinary = urlCloudinary })
                        .ToListAsync(cancellationToken);

                    citiesDtoSearch = citiesDtoSearch.Select(x =>
                    {
                        x.IsActive = x.Name.Contains(request.Search, StringComparison.OrdinalIgnoreCase);
                        return x;
                    })
                    .OrderByDescending(x => x.IsActive)
                    .ToList();

                    if (citiesDtoSearch.All(x => !x.IsActive)) citiesDtoSearch.Clear();

                    await _redisCacheAccessor.SetCacheValueAsync(citiesDtoSearch, keyMaster);
                }

                return Result<List<CityDtoQuery>>.Success(citiesDtoSearch);
            }

            var citiesDto = await _redisCacheAccessor.GetCacheValueAsync<List<CityDtoQuery>>(keyMaster);

            if (citiesDto == null)
            {
                citiesDto = await query
                    .ProjectTo<CityDtoQuery>(_mapper.ConfigurationProvider, new { currentUrlCloudinary = urlCloudinary })
                    .ToListAsync(cancellationToken);

                await _redisCacheAccessor.SetCacheValueAsync(citiesDto, keyMaster);
            }

            return Result<List<CityDtoQuery>>.Success(citiesDto);
        }
    }
}