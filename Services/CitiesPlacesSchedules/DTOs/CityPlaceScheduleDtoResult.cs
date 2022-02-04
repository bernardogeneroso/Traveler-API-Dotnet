using Models;

namespace Services.CitiesPlacesSchedules.DTOs;

public class CityPlaceScheduleDtoResult
{
    public DayWeek DayWeek { get; set; }
    public float? StartTime { get; set; }
    public float? EndTime { get; set; }
}