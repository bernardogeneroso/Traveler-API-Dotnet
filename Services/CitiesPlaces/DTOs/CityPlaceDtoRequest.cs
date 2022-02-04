using Microsoft.AspNetCore.Http;

namespace Services.CitiesPlaces.DTOs;

public class CityPlaceDtoRequest
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Description { get; set; }
    public IFormFile File { get; set; }
}
