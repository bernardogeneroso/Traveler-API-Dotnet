using Microsoft.AspNetCore.Http;

namespace Services.CitiesCategories.DTOs;

public class CategoryCityDtoRequest
{
    public string Name { get; set; }
    public IFormFile File { get; set; }
}
