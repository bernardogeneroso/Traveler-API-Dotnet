using Models;

namespace Services.CitiesPlacesSchedules.DTOs;

public class CityPlaceScheduleQuery
{
    public Guid PlaceId { get; set; }
    public DayWeek DayWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsClosed => StartTime == default && EndTime == default;
}