using FluentValidation;
using Services.Cities.DTOs;
using Services.CitiesDetails;

namespace Services.Cities;

public class CityValidator : AbstractValidator<CityDtoRequest>
{
    public CityValidator()
    {
        RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required");
        RuleFor(x => x.Locations)
                .GreaterThan(0)
                .WithMessage("Locations must be greater than 0")
                .NotNull()
                .WithMessage("Locations is required");
        RuleFor(x => x.Detail)
                .SetValidator(new CityDetailValidator())
                .NotNull()
                .WithMessage("Detail is required");
    }
}
