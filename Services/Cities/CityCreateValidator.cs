using FluentValidation;
using Services.Cities.DTOs;

namespace Services.Cities;

public class CityCreateValidator : AbstractValidator<CityDtoCreateRequest>
{
    public CityCreateValidator()
    {
        RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("Name is required")
                .NotEmpty()
                .WithMessage("Name is required");
        RuleFor(x => x.Locations)
                .GreaterThan(0)
                .WithMessage("Locations must be greater than 0")
                .NotNull()
                .WithMessage("Locations is required")
                .NotEmpty()
                .WithMessage("Locations is required");
        RuleFor(x => x.Description)
                .NotNull()
                .WithMessage("Description is required")
                .NotEmpty()
                .WithMessage("Description is required");
        RuleFor(x => x.SubDescription)
                .NotNull()
                .WithMessage("Sub description is required")
                .NotEmpty()
                .WithMessage("Sub description is required");
        RuleFor(x => x.File.Length)
                .NotNull()
                .WithMessage("File is required")
                .NotEmpty()
                .WithMessage("File is required")
                .LessThanOrEqualTo(1 * 1024 * 1024) // 1mb
                .WithMessage("File size is larger than allowed limit 1MB");
        RuleFor(x => x.File.ContentType)
                .Must(x => x.Contains("image"))
                .WithMessage("File must be an image")
                .NotEmpty();
    }
}
