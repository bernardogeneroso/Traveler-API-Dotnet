using Microsoft.AspNetCore.Mvc;
using Services.CitiesPlaces;
using Services.CitiesPlaces.DTOs;
using Services.PlacesMessages.DTOs;

namespace API.Controllers;

[Route("api/cities/places")]
public class CitiesPlacesController : BaseApiController
{
    [HttpGet("{cityId}")]
    public async Task<IActionResult> GetPlaces(Guid cityId, [FromQuery] Guid? categoryId, [FromQuery] bool? topRated)
    {
        return HandleResult(await Mediator.Send(new List.Query { CityId = cityId, CategoryId = categoryId, TopRated = topRated }));
    }

    [HttpGet("{cityId}/{id}")]
    public async Task<IActionResult> GetPlace(Guid cityId, Guid id)
    {
        return HandleResult(await Mediator.Send(new Detail.Query { CityId = cityId, Id = id }));
    }

    [HttpPost("{cityId}/{categoryId}")]
    public async Task<IActionResult> CreatePlace(Guid cityId, Guid categoryId, [FromForm] CityPlaceDtoRequest place)
    {
        return HandleResult(await Mediator.Send(new Create.Command { Place = place, CityId = cityId, CategoryId = categoryId }));
    }

    [HttpPost("{cityId}/setHighlighted/{id}")]
    public async Task<IActionResult> SetPlaceHighlighted(Guid cityId, Guid id)
    {
        return HandleResult(await Mediator.Send(new SetHighlighted.Command { CityId = cityId, Id = id }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
    }

    [HttpPost("messages/{placeId}")]
    public async Task<IActionResult> CreateMessage(Guid placeId, [FromForm] CityPlaceMessageDtoResult commentMessage)
    {
        return HandleResult(await Mediator.Send(new Services.PlacesMessages.Create.Command { PlaceId = placeId, Message = commentMessage }));
    }
}
