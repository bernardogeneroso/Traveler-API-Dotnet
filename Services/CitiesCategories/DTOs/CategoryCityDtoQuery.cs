using Models.Helpers;

namespace Services.CitiesCategories.DTOs;

public class CategoryCityDtoQuery
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Places { get; set; }
    public Image Image { get; set; }
}
