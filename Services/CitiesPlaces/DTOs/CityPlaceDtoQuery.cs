using Models.Helpers;
using Services.CitiesPlacesSchedules.DTOs;

namespace Services.CitiesPlaces.DTOs;

public class CityPlaceDtoQuery
{
    public Guid Id { get; set; }
    public Guid CityId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string PhoneNumber { get; set; }
    public float? Rating { get; set; }
    public Image Image { get; set; }
    public List<CityPlaceScheduleDtoQuery> Schedules { get; set; } = new List<CityPlaceScheduleDtoQuery>();
}