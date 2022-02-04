namespace Models;

public class CityPlaceMessage : BaseEntity
{
    public Guid Id { get; set; }
    public Guid PlaceId { get; set; }
    public string DisplayName { get; set; }
    public string AvatarName { get; set; }
    public string Message { get; set; }
    public int Rating { get; set; } = 0;
    public CityPlace Place { get; set; }
}
