namespace Services.CitiesPlaces.DTOs;

public class CityPlaceDtoRequest
{
    public Guid CityId { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
}
