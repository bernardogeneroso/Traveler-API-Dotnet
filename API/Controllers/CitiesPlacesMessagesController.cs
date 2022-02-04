using Microsoft.AspNetCore.Mvc;
using Services.PlacesMessages;
using Services.PlacesMessages.DTOs;

namespace API.Controllers;

[ApiController]
[Route("api/cities/places/messages")]
public class CitiesPlacesMessagesController : BaseApiController
{
    [HttpPost("{placeId}")]
    public async Task<IActionResult> CreateMessage(Guid placeId, [FromForm] CityPlaceMessageDtoResult message)
    {
        return HandleResult(await Mediator.Send(new Create.Command { PlaceId = placeId, Message = message }));
    }
}
