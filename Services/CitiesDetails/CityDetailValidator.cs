using FluentValidation;
using Services.CitiesDetails.DTOs;

namespace Services.CitiesDetails;

public class CityDetailValidator : AbstractValidator<CityDetailDtoRequest>
{
    public CityDetailValidator()
    {
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.SubDescription).NotEmpty();
    }
}
