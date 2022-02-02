using Microsoft.AspNetCore.Mvc;
using Services.CitiesPlaces;
using Services.CitiesPlaces.DTOs;

namespace API.Controllers;

[Route("api/cities/place")]
public class CitiesPlacesController : BaseApiController
{
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
