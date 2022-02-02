namespace Services.CitiesPlaces.DTOs;

public class CityPlaceDtoQuery
{
    public Guid Id { get; set; }
    public Guid CityId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string PhoneNumber { get; set; }
    public string ImageName { get; set; }
}
