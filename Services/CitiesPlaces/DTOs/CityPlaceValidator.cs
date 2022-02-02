using FluentValidation;

namespace Services.CitiesPlaces.DTOs;

public class CityPlaceValidator : AbstractValidator<CityPlaceDtoRequest>
{
    public CityPlaceValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.CategoryId).NotEmpty();
        RuleFor(x => x.CityId).NotEmpty();
    }
}
