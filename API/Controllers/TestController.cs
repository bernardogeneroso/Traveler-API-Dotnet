using Microsoft.AspNetCore.Mvc;
using Services.PlacesMessages;
using Services.PlacesMessages.DTOs;

namespace API.Controllers;

public class TestController : BaseApiController
{
    // [HttpPost("{placeId}")]
    // public async Task<IActionResult> CreateMessageTest(Guid placeId, CityPlaceMessageDtoResult message)
    // {
    //     return HandleResult(await Mediator.Send(new Create.Command { PlaceId = placeId, Message = message }));
    // }

    // [HttpPost("imageTest")]
    // public async Task<IActionResult> UploadImageTest([FromForm] IFormFile File, [FromForm] string Test, [FromForm] string Test2)
    // {
    //     return HandleResult(await Mediator.Send(new Services.Cities.UploadImage.Command { File = File, Test = Test, Test2 = Test2 }));
    // }
}
