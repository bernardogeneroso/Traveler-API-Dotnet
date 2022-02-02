using Microsoft.AspNetCore.Mvc;
using Services.CitiesPlacesSchedules.DTOs;

namespace API.Controllers;

[ApiController]
[Route("api/cities/places/schedules")]
public class CitiesPlacesSchedulesController : BaseApiController
{
    [HttpPost("{placeId}")]
    public async Task<IActionResult> CreatePlaceScheduling(Guid placeId, [FromBody] CityPlaceScheduleResult placeSchedule)
    {
        return HandleResult(await Mediator.Send(new Services.CitiesPlacesSchedules.Create.Command { PlaceSchedule = placeSchedule, PlaceId = placeId }));
    }
}
