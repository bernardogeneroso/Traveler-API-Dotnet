using Services.CitiesCategories.DTOs;
using Services.CitiesPlaces.DTOs;

namespace Services.Cities.DTOs;

public class CityDtoGetQuery
{
    public CityDtoDetailsQuery City { get; set; }
    public CityPlaceDtoHighlightQuery PlaceHighlighted { get; set; } = null;
    public List<CategoryCityDtoQuery> Categories { get; set; } = new List<CategoryCityDtoQuery>();
}