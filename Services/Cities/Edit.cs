using Application.Core;
using AutoMapper;
using Database;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.Cities.DTOs;

namespace Services.Cities;

public class Edit
{
    public class Command : IRequest<Result<Unit>>
    {
        public CityDtoRequest City { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.City).SetValidator(new CityValidator()).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public Handler(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var city = await _context.Cities
                    .Include(x => x.Detail)
                    .FirstOrDefaultAsync(x => x.Id == request.City.Id);

            if (city == null) return Result<Unit>.Failure("Couldn't find the city");

            _mapper.Map(request.City, city);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Couldn't update the city");

            return Result<Unit>.SuccessNoContent(Unit.Value);
        }
    }
}
