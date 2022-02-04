using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Database;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.Cities.DTOs;

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
        private readonly IConfiguration _config;

        public Handler(DataContext context, IMapper mapper, IConfiguration config)
        {
            _config = config;
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

            var urlCloudinary = _config.GetSection("Cloudinary").GetValue<string>("Url");

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var cities = await query
                        .AsNoTracking()
                        .ProjectTo<CityDtoQuery>(_mapper.ConfigurationProvider, new { currentUrlCloudinary = urlCloudinary })
                        .ToListAsync(cancellationToken);

                cities = cities.Select(x =>
                {
                    x.IsActive = x.Name.Contains(request.Search, StringComparison.OrdinalIgnoreCase);
                    return x;
                })
                .OrderByDescending(x => x.IsActive)
                .ToList();

                if (cities.All(x => !x.IsActive)) cities.Clear();

                return Result<List<CityDtoQuery>>.Success(cities);
            }

            return Result<List<CityDtoQuery>>.Success(
                await query
                .ProjectTo<CityDtoQuery>(_mapper.ConfigurationProvider, new { currentUrlCloudinary = urlCloudinary })
                .ToListAsync(cancellationToken)
            );
        }
    }
}