namespace Services.CitiesPlaces.DTOs;

public class CityPlaceDtoListQuery
{
    public Guid Id { get; set; }
    public Guid CityId { get; set; }
    public string Name { get; set; }
    public string ImageName { get; set; }
}
