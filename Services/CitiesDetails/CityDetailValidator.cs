using FluentValidation;
using Models;

namespace Services.CitiesDetails;

public class CityDetailValidator : AbstractValidator<CityDetail>
{
    public CityDetailValidator()
    {
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.SubDescription).NotEmpty();
    }
}
