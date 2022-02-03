namespace Models;

public class CityPlaceSchedule : BaseEntity
{
    public Guid Id { get; set; }
    public Guid PlaceId { get; set; }
    public DayWeek DayWeek { get; set; }
    public float? StartTime { get; set; }
    public float? EndTime { get; set; }
    public bool IsClosed => StartTime == null && EndTime == null;
    public CityPlace Place { get; set; }
}

public enum DayWeek
{
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    Sunday
}
