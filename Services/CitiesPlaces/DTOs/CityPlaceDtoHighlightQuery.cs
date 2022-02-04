using Models.Helpers;

namespace Services.CitiesPlaces.DTOs;

public class CityPlaceDtoHighlightQuery
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public Guid CityId { get; set; }
    public string Name { get; set; }
    public float? Rating { get; set; }
    public bool IsHighlighted { get; set; }
    public Image Image { get; set; }
}
