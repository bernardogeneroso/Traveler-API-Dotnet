using Application.Core;
using AutoMapper;
using Database;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.CitiesPlacesSchedules.DTOs;

namespace Services.CitiesPlacesSchedules;

public class Edit
{
    public class Command : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
        public Guid PlaceId { get; set; }
        public CityPlaceScheduleResult Schedule { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Schedule).SetValidator(new CityPlaceScheduleValidator()).NotEmpty();
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
            var existSchedule = await _context.CitiesPlacesSchedules
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.PlaceId == request.PlaceId, cancellationToken);

            if (existSchedule == null) return Result<Unit>.Failure("Schedule not found");

            _mapper.Map(request.Schedule, existSchedule);

            _context.CitiesPlacesSchedules.Update(existSchedule);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Fail to update schedule");

            return Result<Unit>.SuccessNoContent(Unit.Value);
        }
    }
}
