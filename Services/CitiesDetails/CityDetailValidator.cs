using FluentValidation;
using Services.CitiesDetails.DTOs;

namespace Services.CitiesDetails;

public class CityDetailValidator : AbstractValidator<CityDetailDtoRequest>
{
    public CityDetailValidator()
    {
        RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required");
        RuleFor(x => x.SubDescription)
                .NotEmpty()
                .WithMessage("Sub description is required");
    }
}
