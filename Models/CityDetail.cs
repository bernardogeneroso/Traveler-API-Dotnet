namespace Models;

public class CityDetail
{
    public Guid CityId { get; set; }
    public string Description { get; set; }
    public string SubDescription { get; set; }
    public City City { get; set; }
}
