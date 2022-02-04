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
                .WithMessage("Name is required")
                .NotNull()
                .WithMessage("Name is required");
        RuleFor(x => x.Locations)
                .GreaterThan(0)
                .WithMessage("Locations must be greater than 0")
                .NotEmpty()
                .WithMessage("Locations is required")
                .NotNull()
                .WithMessage("Locations is required");
        RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required")
                .NotNull()
                .WithMessage("Description is required");
        RuleFor(x => x.SubDescription)
                .NotEmpty()
                .WithMessage("Sub description is required")
                .NotNull()
                .WithMessage("Sub description is required");
    }
}
