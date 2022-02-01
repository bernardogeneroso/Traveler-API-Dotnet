using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.Cities.DTOs;
using Services.Interfaces;

namespace Services.Cities;

public class Details
{
    public class Query : IRequest<Result<CityDtoQuery>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<CityDtoQuery>>
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

        public async Task<Result<CityDtoQuery>> Handle(Query request, CancellationToken cancellationToken)
        {
            var cityDto = await _context.Cities
                        .Include(x => x.Detail)
                        .ProjectTo<CityDtoQuery>(_mapper.ConfigurationProvider, new { currentOrigin = _originAccessor.GetOrigin() })
                        .FirstOrDefaultAsync(x => x.Id == request.Id);

            return Result<CityDtoQuery>.Success(cityDto);
        }
    }
}
