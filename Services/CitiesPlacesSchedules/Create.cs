using Application.Core;
using AutoMapper;
using Database;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.CitiesPlacesSchedules.DTOs;

namespace Services.CitiesPlacesSchedules;

public class Create
{
    public class Command : IRequest<Result<Unit>>
    {
        public Guid PlaceId { get; set; }
        public CityPlaceScheduleResult PlaceSchedule { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.PlaceSchedule).SetValidator(new CityPlaceScheduleValidator()).NotEmpty();
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
            if (!await _context.CitiesPlaces.AnyAsync(x => x.Id == request.PlaceId))
                return Result<Unit>.Failure("City place not found");

            if (await _context.CitiesPlacesSchedules.CountAsync(x => x.PlaceId == request.PlaceId) == 7)
                return Result<Unit>.Failure("City place already has 7 schedules");

            if (await _context.CitiesPlacesSchedules.AnyAsync(x => x.PlaceId == request.PlaceId && x.DayWeek == request.PlaceSchedule.DayWeek))
                return Result<Unit>.Failure("City place already has a schedule for this day");

            var cityPlaceSchedule = _mapper.Map<CityPlaceSchedule>(request.PlaceSchedule);

            cityPlaceSchedule.PlaceId = request.PlaceId;

            _context.CitiesPlacesSchedules.Add(cityPlaceSchedule);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Could not create city place schedule");

            return Result<Unit>.SuccessNoContent(Unit.Value);
        }
    }
}