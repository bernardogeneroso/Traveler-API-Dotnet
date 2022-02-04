using Models.Helpers;
using Services.CitiesDetails;

namespace Services.Cities.DTOs;

public class CityDtoDetailsQuery
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ImageDto Image { get; set; }
    public CityDetailDtoQuery Detail { get; set; }
}
