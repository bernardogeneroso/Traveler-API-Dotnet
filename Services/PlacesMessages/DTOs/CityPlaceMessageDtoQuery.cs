using Models.Helpers;

namespace Services.PlacesMessages.DTOs;

public class CityPlaceMessageDtoQuery
{
    public Guid Id { get; set; }
    public Guid PlaceId { get; set; }
    public string DisplayName { get; set; }
    public string Message { get; set; }
    public int Rating { get; set; } = 0;
    public Avatar Avatar { get; set; }
}