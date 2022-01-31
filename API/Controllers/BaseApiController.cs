using Application.Core;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    private IMediator _mediator { get; set; } = default!;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>() ?? throw new System.Exception("Mediator not found");

    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if (result == null)
            return NotFound();

        if (result.IsSuccess && result.IsSuccessNoContent)
            return NoContent();

        if (result.IsSuccess && result.Value != null)
            return Ok(result.Value);

        if (result.IsSuccess && result.Value == null)
            return NotFound();

        if (result.Error != null && result.FluentValidationError == null)
            return BadRequest(result.Error);


        result.FluentValidationError.AddToModelState(ModelState, null);
        return ValidationProblem();
    }
}
