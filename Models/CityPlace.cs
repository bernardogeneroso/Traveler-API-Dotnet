namespace Models;

public class CityPlace
{
    public Guid Id { get; set; }
    public Guid CityId { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string ImageName { get; set; }
    public City City { get; set; }
    public CategoryCity Category { get; set; }
}
