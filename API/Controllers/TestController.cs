using Microsoft.AspNetCore.Mvc;
using Services.PlacesMessages;
using Services.PlacesMessages.DTOs;

namespace API.Controllers;

public class TestController : BaseApiController
{
    [HttpPost("{placeId}")]
    public async Task<IActionResult> CreateMessageTest(Guid placeId, CityPlaceMessageDtoResult message)
    {
        return HandleResult(await Mediator.Send(new Create.Command { PlaceId = placeId, Message = message }));
    }
}
