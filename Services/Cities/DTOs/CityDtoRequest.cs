using Services.CitiesDetails.DTOs;

namespace Services.Cities.DTOs;

public class CityDtoRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Locations { get; set; }
    public CityDetailDtoRequest Detail { get; set; }
}
