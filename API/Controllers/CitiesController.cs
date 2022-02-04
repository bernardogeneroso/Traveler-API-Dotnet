using Microsoft.AspNetCore.Mvc;
using Services.Cities;
using Services.Cities.DTOs;

namespace API.Controllers;

public class CitiesController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetCities([FromQuery] int filter = 0, string search = null)
    {
        return HandleResult(await Mediator.Send(new List.Query { Filter = filter, Search = search }));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCity(Guid id)
    {
        return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditCity(Guid id, [FromForm] CityDtoRequest City)
    {
        return HandleResult(await Mediator.Send(new Edit.Command { Id = id, City = City }));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCity([FromForm] CityDtoCreateRequest city)
    {
        return HandleResult(await Mediator.Send(new Create.Command { City = city }));
    }

    [HttpPost("{id}/click")]
    public async Task<IActionResult> ClickCity(Guid id)
    {
        return HandleResult(await Mediator.Send(new CityClicked.Command { Id = id }));
    }
}