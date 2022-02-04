using Microsoft.AspNetCore.Http;

namespace Services.PlacesMessages.DTOs;

public class CityPlaceMessageDtoResult
{
    public string DisplayName { get; set; }
    public string Message { get; set; }
    public int Rating { get; set; } = 0;
    public IFormFile File { get; set; }
}
