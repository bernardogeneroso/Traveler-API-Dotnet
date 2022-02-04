using FluentValidation;
using Services.PlacesMessages.DTOs;

namespace Services.PlacesMessages;

public class CityPlaceMessageValidator : AbstractValidator<CityPlaceMessageDtoResult>
{
    public CityPlaceMessageValidator()
    {
        RuleFor(x => x.DisplayName)
            .NotEmpty()
            .WithMessage("Display name is required")
            .Length(2, 50)
            .WithMessage("DisplayName must be between 2 and 50 characters");
        RuleFor(x => x.Message)
            .NotEmpty()
            .WithMessage("Message is required")
            .MaximumLength(500)
            .WithMessage("Message must be between 1 and 500 characters");
        RuleFor(x => x.Rating)
            .InclusiveBetween(0, 5)
            .WithMessage("Rating must be between 0 and 5")
            .NotEmpty()
            .WithMessage("Rating is required");
    }
}
