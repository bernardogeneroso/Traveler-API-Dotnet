using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Cities;

namespace API.Controllers;

public class CitiesController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetCities()
    {
        return HandleResult(await Mediator.Send(new List.Query()));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCity([FromBody] City city)
    {
        return HandleResult(await Mediator.Send(new Create.Command { City = city }));
    }
}
