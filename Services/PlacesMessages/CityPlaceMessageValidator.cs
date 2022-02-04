using FluentValidation;
using Services.PlacesMessages.DTOs;

namespace Services.PlacesMessages;

public class CityPlaceMessageValidator : AbstractValidator<CityPlaceMessageDtoResult>
{
    public CityPlaceMessageValidator()
    {
        RuleFor(x => x.DisplayName)
            .NotNull()
            .WithMessage("Display name is required")
            .NotEmpty()
            .WithMessage("Display name is required")
            .Length(2, 50)
            .WithMessage("Display name must be between 2 and 50 characters");
        RuleFor(x => x.Message)
            .NotNull()
            .WithMessage("Message is required")
            .NotEmpty()
            .WithMessage("Message is required")
            .Length(2, 50)
            .WithMessage("Message must be between 2 and 50 characters");
        RuleFor(x => x.Rating)
            .InclusiveBetween(0, 5)
            .WithMessage("Rating must be between 0 and 5");
    }
}
