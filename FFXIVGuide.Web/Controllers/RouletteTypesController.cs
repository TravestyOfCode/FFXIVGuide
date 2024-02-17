using FFXIVGuide.Web.Data.Result;
using FFXIVGuide.Web.Data.RouletteType;
using FFXIVGuide.Web.Data.RouletteType.Commands;
using FFXIVGuide.Web.Data.RouletteType.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FFXIVGuide.Web.Controllers;

[Authorize(Roles = "Admin")]
public class RouletteTypesController : Controller
{
    private readonly IMediator _mediator;

    public RouletteTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Index(GetAllRouletteTypes request, CancellationToken cancellationToken)
    {
        var results = await _mediator.Send(request, cancellationToken);

        return results.WasSuccess ? View(results.Value) : StatusCode(results.StatusCode);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return PartialView("CreateModal");
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRouletteType request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return PartialView("CreateModal");
        }

        var result = await _mediator.Send(request, cancellationToken);

        if (result.WasSuccess)
        {
            Response.Headers.Add("HX-Refresh", "true");
            return Ok();
        }

        if (result.WasBadRequest)
        {
            ModelState.AddErrors(result);
            return PartialView("CreateModal");
        }

        return StatusCode(result.StatusCode);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetRouletteTypeById(id), cancellationToken);

        if (result.WasSuccess)
        {
            return PartialView("EditingRow", result.Value);
        }

        return StatusCode(result.StatusCode);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateRouletteType request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return PartialView("EditingRow");
        }

        var result = await _mediator.Send(request, cancellationToken);

        if (result.WasSuccess)
        {
            return PartialView("EditableRow", result.Value);
        }

        if (result.WasBadRequest)
        {
            ModelState.AddErrors(result);

            return PartialView("EditingRow");
        }

        return StatusCode(result.StatusCode);

    }

    [HttpGet]
    public async Task<IActionResult> CancelUpdate(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetRouletteTypeById(id), cancellationToken);

        if (result.WasSuccess)
        {
            return PartialView("EditableRow", result.Value);
        }

        return StatusCode(result.StatusCode);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteRouletteType(id), cancellationToken);

        if (result.WasSuccess)
        {
            return Ok();
        }

        return StatusCode(result.StatusCode);
    }
}
