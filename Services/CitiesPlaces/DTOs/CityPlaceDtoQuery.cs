using Services.CitiesCategories.DTOs;

namespace Services.CitiesPlaces.DTOs;

public class CityPlaceDtoQuery
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImageName { get; set; }
    public CategoryCityDtoQuery CityCategoryDtoQuery { get; set; }
}
