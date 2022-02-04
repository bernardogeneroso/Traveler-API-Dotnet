using Models.Helpers;

namespace Services.CitiesPlaces.DTOs;

public class CityPlaceDtoListQuery
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public float? Rating { get; set; }
    public ImageDto Image { get; set; }
}