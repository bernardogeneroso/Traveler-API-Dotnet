namespace Models;

public class CategoryCity : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Places { get; set; } = 0;
    public string ImageName { get; set; }
    public ICollection<CityPlace> CitiesPlaces { get; set; } = new List<CityPlace>();
}
