using FluentValidation;
using Services.CitiesPlaces.DTOs;

namespace Services.CitiesPlaces;

public class CityPlaceValidator : AbstractValidator<CityPlaceDtoRequest>
{
    public CityPlaceValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .NotNull()
            .WithMessage("Name is required");
        RuleFor(x => x.PhoneNumber)
            .Matches(@"9[1236][0-9]{7}|2[1-9]{1,2}[0-9]{7}")
            .WithMessage("Phone number is not valid")
            .NotEmpty()
            .WithMessage("Phone number is required")
            .NotNull()
            .WithMessage("Phone number is required");
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required")
            .NotNull()
            .WithMessage("Description is required");
    }
}
