using FluentValidation;
using Services.CitiesCategories.DTOs;

namespace Services.CitiesCategories;

public class CategoryCityValidator : AbstractValidator<CategoryCityDtoRequest>
{
    public CategoryCityValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");
    }
}
