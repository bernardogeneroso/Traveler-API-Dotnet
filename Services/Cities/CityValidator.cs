using FluentValidation;
using Services.Cities.DTOs;
using Services.CitiesDetails;

namespace Services.Cities;

public class CityValidator : AbstractValidator<CityDtoRequest>
{
    public CityValidator()
    {
        RuleFor(x => x.Name)
                .NotEmpty();
        RuleFor(x => x.Locations)
                .GreaterThan(0)
                .WithMessage("The locations must be a number")
                .NotEmpty();
        RuleFor(x => x.Detail)
                .SetValidator(new CityDetailValidator())
                .NotEmpty();
    }
}
