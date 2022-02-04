using Microsoft.AspNetCore.Mvc;
using Services.CitiesCategories;
using Services.CitiesCategories.DTOs;

namespace API.Controllers;

[Route("api/category/cities")]
public class CategoryCitiesController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromForm] CategoryCityDtoRequest category)
    {
        return HandleResult(await Mediator.Send(new Create.Command { Category = category }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
    }
}
