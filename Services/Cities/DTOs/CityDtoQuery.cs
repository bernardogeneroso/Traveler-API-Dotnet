using Models.Helpers;
using Services.CitiesDetails;

namespace Services.Cities.DTOs;

public class CityDtoQuery
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Locations { get; set; }
    public Image Image { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public CityDetailDtoQuery Detail { get; set; }
}