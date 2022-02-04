namespace Services.PlacesMessages.DTOs;

public class CityPlaceMessageDtoCommand
{
    public CityPlaceMessageDtoQuery Message { get; set; }
    public float? PlaceRating { get; set; }
}
