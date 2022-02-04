namespace Models;

public class City : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Locations { get; set; }
    public string ImageName { get; set; }
    public string ImagePublicId { get; set; }
    public int ClickedCount { get; set; } = 0;
    public CityDetail Detail { get; set; }
    public ICollection<CityPlace> CitiesPlaces { get; set; } = new List<CityPlace>();
}
