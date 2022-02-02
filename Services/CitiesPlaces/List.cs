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
        public Guid CategoryId { get; set; }
    }

    public class QueryValidator : AbstractValidator<Query>
    {
        public QueryValidator()
        {
            RuleFor(x => x.CityId).NotEmpty();
            RuleFor(x => x.CategoryId).NotEmpty();
        }
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
            var cityPlaces = _context.CitiesPlaces
                .Where(c => c.CityId == request.CityId)
                .AsQueryable();

            if (request.CategoryId != Guid.Empty)
            {
                cityPlaces = cityPlaces.Where(x => x.CategoryId == request.CategoryId);
            }

            return Result<List<CityPlaceDtoListQuery>>.Success(
                await cityPlaces
                    .ProjectTo<CityPlaceDtoListQuery>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            );
        }
    }
}
