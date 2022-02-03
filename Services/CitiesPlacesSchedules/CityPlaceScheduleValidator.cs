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
                .Cascade(CascadeMode.Stop)
                .Null()
                .When(x => x.EndTime == null)
                .WithMessage("Start time is required")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Start time must be greater than or equal to 0")
                .LessThanOrEqualTo(23)
                .WithMessage("Start time must be less than or equal to 23")
                .Must((x, y) => x.EndTime == null || x.StartTime < x.EndTime)
                .WithMessage("Start time must be less than end time");
        RuleFor(x => x.EndTime)
                .Cascade(CascadeMode.Stop)
                .Null()
                .When(x => x.StartTime == null)
                .WithMessage("End time is required")
                .LessThanOrEqualTo(24)
                .WithMessage("End time must be less than or equal to 24")
                .GreaterThanOrEqualTo(1)
                .WithMessage("End time must be greater than or equal to 1")
                .Must((x, y) => x.StartTime == null || x.StartTime < x.EndTime)
                .WithMessage("End time must be greater than start time");

    }
}
