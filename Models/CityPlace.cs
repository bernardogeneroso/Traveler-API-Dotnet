namespace Models;

public class CityPlace : BaseEntity
{
    public Guid Id { get; set; }
    public Guid CityId { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string ImageName { get; set; }
    public string Description { get; set; }
    public string PhoneNumber { get; set; }
    public float? Rating { get; set; }
    public bool IsHighlighted { get; set; }
    public City City { get; set; }
    public CategoryCity Category { get; set; }
    public ICollection<CityPlaceSchedule> Schedules { get; set; } = new List<CityPlaceSchedule>();
    public ICollection<CityPlaceMessage> Messages { get; set; } = new List<CityPlaceMessage>();
}
