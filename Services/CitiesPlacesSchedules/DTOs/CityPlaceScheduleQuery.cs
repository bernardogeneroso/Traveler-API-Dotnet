using Models;

namespace Services.CitiesPlacesSchedules.DTOs;

public class CityPlaceScheduleQuery
{
    public Guid Id { get; set; }
    public DayWeek DayWeek { get; set; }
    public float? StartTime { get; set; }
    public float? EndTime { get; set; }
    public bool IsClosed => StartTime == null && EndTime == null;
}