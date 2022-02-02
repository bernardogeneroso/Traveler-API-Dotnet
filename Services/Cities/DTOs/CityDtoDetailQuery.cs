using Services.CitiesCategories.DTOs;

namespace Services.Cities.DTOs;

public class CityDtoDetailQuery
{
    public CityDtoQuery City { get; set; }
    public List<CategoryCityDtoQuery> Categories { get; set; } = new List<CategoryCityDtoQuery>();
}
