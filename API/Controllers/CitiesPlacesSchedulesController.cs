using Microsoft.AspNetCore.Mvc;
using Services.CitiesPlacesSchedules.DTOs;

namespace API.Controllers;

[ApiController]
[Route("api/cities/places/schedules")]
public class CitiesPlacesSchedulesController : BaseApiController
{
    [HttpPost("{placeId}")]
    public async Task<IActionResult> CreatePlaceScheduling(Guid placeId, [FromBody] CityPlaceScheduleDtoResult schedule)
    {
        return HandleResult(await Mediator.Send(new Services.CitiesPlacesSchedules.Create.Command { Schedule = schedule, PlaceId = placeId }));
    }

    [HttpPut("{placeId}/{id}")]
    public async Task<IActionResult> EditPlaceScheduling(Guid placeId, Guid id, [FromBody] CityPlaceScheduleDtoResult schedule)
    {
        return HandleResult(await Mediator.Send(new Services.CitiesPlacesSchedules.Edit.Command { Schedule = schedule, Id = id, PlaceId = placeId }));
    }

    [HttpDelete("{placeId}/{id}")]
    public async Task<IActionResult> DeletePlaceScheduling(Guid placeId, Guid id)
    {
        return HandleResult(await Mediator.Send(new Services.CitiesPlacesSchedules.Delete.Command { Id = id, PlaceId = placeId }));
    }
}
