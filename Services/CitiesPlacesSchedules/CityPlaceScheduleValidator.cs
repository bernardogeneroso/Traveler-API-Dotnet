using FluentValidation;
using Services.CitiesPlacesSchedules.DTOs;

namespace Services.CitiesPlacesSchedules;

public class CityPlaceScheduleValidator : AbstractValidator<CityPlaceScheduleResult>
{
    public CityPlaceScheduleValidator()
    {
        RuleFor(x => x.DayWeek)
                .IsInEnum()
                .WithMessage("Day of week is not valid (0-6)")
                .NotNull()
                .WithMessage("Day of week is required");
        RuleFor(x => x.StartTime)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Start time must be greater than or equal to 0")
                .LessThanOrEqualTo(23)
                .WithMessage("Start time must be less than or equal to 23")
                .NotNull()
                .WithMessage("Start time is required");
        RuleFor(x => x.EndTime)
                .LessThanOrEqualTo(24)
                .WithMessage("End time must be less than or equal to 24")
                .GreaterThanOrEqualTo(1)
                .WithMessage("End time must be greater than or equal to 1")
                .Must((x, y) => x.StartTime < x.EndTime)
                .WithMessage("End time must be greater than start time")
                .NotNull()
                .WithMessage("End time is required");
    }
}
