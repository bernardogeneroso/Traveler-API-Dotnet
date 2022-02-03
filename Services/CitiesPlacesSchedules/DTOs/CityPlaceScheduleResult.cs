using Models;

namespace Services.CitiesPlacesSchedules.DTOs;

public class CityPlaceScheduleResult
{
    public DayWeek DayWeek { get; set; }
    public float? StartTime { get; set; }
    public float? EndTime { get; set; }
}