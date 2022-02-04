using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Database;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.CitiesPlaces.DTOs;
using Services.Interfaces;

namespace Services.CitiesPlaces;

public class List
{
    public class Query : IRequest<Result<List<CityPlaceDtoListQuery>>>
    {
        public Guid CityId { get; set; }
        public Guid? CategoryId { get; set; }
        public bool? TopRated { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<List<CityPlaceDtoListQuery>>>
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

        public async Task<Result<List<CityPlaceDtoListQuery>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var cityPlaces = _context.CityPlace
                .AsNoTracking()
                .Where(c => c.CityId == request.CityId)
                .AsQueryable();

            if (request.CategoryId != null)
            {
                cityPlaces = cityPlaces.Where(x => x.CategoryId == request.CategoryId);
            }

            if (request.TopRated != null && request.TopRated == true)
            {
                cityPlaces = cityPlaces
                        .OrderByDescending(x => x.Rating != null)
                        .ThenByDescending(x => x.Rating)
                        .Take(4);
            }

            return Result<List<CityPlaceDtoListQuery>>.Success(
                await cityPlaces
                    .ProjectTo<CityPlaceDtoListQuery>(_mapper.ConfigurationProvider, new { currentOrigin = _originAccessor.GetOrigin() })
                    .ToListAsync(cancellationToken)
            );
        }
    }
}
