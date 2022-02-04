using Microsoft.AspNetCore.Http;

namespace Services.Cities.DTOs;

public class CityDtoCreateRequest
{
    public string Name { get; set; }
    public int Locations { get; set; }
    public string Description { get; set; }
    public string SubDescription { get; set; }
    public IFormFile File { get; set; }
}