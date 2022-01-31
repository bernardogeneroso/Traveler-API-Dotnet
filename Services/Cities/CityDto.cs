using Models;

namespace Services.Cities;

public class CityDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Locations { get; set; } = int.MaxValue;
    public string ImageName { get; set; } = string.Empty;
    public int ClickedCount { get; set; } = int.MaxValue;
    public CityDetail Detail { get; set; } = new CityDetail();
}
