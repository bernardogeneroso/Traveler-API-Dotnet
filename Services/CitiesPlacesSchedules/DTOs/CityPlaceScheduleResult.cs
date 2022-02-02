using Models;

namespace Services.CitiesPlacesSchedules.DTOs;

public class CityPlaceScheduleResult
{
    public DayWeek DayWeek { get; set; }
    public Double StartTime { get; set; }
    public Double EndTime { get; set; }
}