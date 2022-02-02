using Microsoft.AspNetCore.Mvc;
using Services.CitiesPlaces;
using Services.CitiesPlaces.DTOs;

namespace API.Controllers;

[Route("api/cities/places")]
public class CitiesPlacesController : BaseApiController
{
    [HttpGet("{cityId}")]
    public async Task<IActionResult> GetPlaces(Guid cityId, [FromQuery] Guid categoryId)
    {
        return HandleResult(await Mediator.Send(new List.Query { CityId = cityId, CategoryId = categoryId }));
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlace([FromBody] CityPlaceDtoRequest place)
    {
        return HandleResult(await Mediator.Send(new Create.Command { Place = place }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
    }
}
