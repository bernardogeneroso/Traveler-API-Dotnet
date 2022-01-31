using FluentValidation;
using Models;
using Services.CitiesDetails;

namespace Services.Cities;

public class CityValidator : AbstractValidator<City>
{
    public CityValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Locations).NotEmpty();
        RuleFor(x => x.ImageName).Empty();
        RuleFor(x => x.ClickedCount).Empty();
        RuleFor(x => x.Detail).SetValidator(new CityDetailValidator());
    }
}
