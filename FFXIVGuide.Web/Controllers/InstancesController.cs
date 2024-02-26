using FFXIVGuide.Web.Data.Instance.Commands;
using FFXIVGuide.Web.Data.Instance.Queries;
using FFXIVGuide.Web.Data.RouletteType.Queries;
using FFXIVGuide.Web.Models.Instances;
using FFXIVGuide.Web.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace FFXIVGuide.Web.Controllers;

public class InstancesController : Controller
{
    private readonly IMediator _mediator;

    public InstancesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Index(GetInstancesPaged request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);

        if (result.WasSuccess)
        {
            return Request.IsHXRequest() ? PartialView(ViewNames.DataList, result.Value) : View(result.Value);
        }

        return StatusCode(result.StatusCode);
    }

    [HttpGet]
    public async Task<IActionResult> Create(GetAllRouletteTypesAsDict request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);

        if (result.WasSuccess)
        {
            return PartialView("CreateModal", new CreateModalModel() { RouletteTypes = result.Value });
        }

        return StatusCode(result.StatusCode);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateInstance request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var roulettes = await _mediator.Send(new GetAllRouletteTypesAsDict(), cancellationToken);

            if (roulettes.WasSuccess)
            {
                return PartialView("CreateModal", new CreateModalModel() { RouletteTypes = roulettes.Value });
            }

            return StatusCode(roulettes.StatusCode);
        }

        var result = await _mediator.Send(request, cancellationToken);

        if (result.WasSuccess)
        {
            Response.Headers.Add(HX.Refresh, "true");
            return Ok();
        }

        if (result.WasBadRequest)
        {
            // Add our errors and get the roulette types again
            ModelState.AddErrors(result);

            var roulettes = await _mediator.Send(new GetAllRouletteTypesAsDict(), cancellationToken);

            if (roulettes.WasSuccess)
            {
                return PartialView("CreateModal", new CreateModalModel() { RouletteTypes = roulettes.Value });
            }

            return StatusCode(roulettes.StatusCode);
        }

        return StatusCode(result.StatusCode);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id, CancellationToken cancellationToken)
    {
        var rouletteTypes = await _mediator.Send(new GetAllRouletteTypesAsDict(), cancellationToken);

        if (rouletteTypes.WasFailure)
        {
            return StatusCode(rouletteTypes.StatusCode);
        }

        var result = await _mediator.Send(new GetInstanceById(id), cancellationToken);

        if (result.WasSuccess)
        {
            return PartialView(ViewNames.EditingRow, new EditingRowModel(result.Value, rouletteTypes.Value));
        }

        return StatusCode(result.StatusCode);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateInstance request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var rouletteTypes = await _mediator.Send(new GetAllRouletteTypesAsDict(), cancellationToken);

            if (rouletteTypes.WasSuccess)
            {
                return PartialView(ViewNames.EditingRow, new EditingRowModel() { RouletteTypes = rouletteTypes.Value });
            }

            return StatusCode(rouletteTypes.StatusCode);
        }

        var result = await _mediator.Send(request, cancellationToken);

        if (result.WasSuccess)
        {
            return PartialView(ViewNames.EditableRow, result.Value);
        }

        if (result.WasBadRequest)
        {
            ModelState.AddErrors(result);

            var rouletteTypes = await _mediator.Send(new GetAllRouletteTypesAsDict(), cancellationToken);

            if (rouletteTypes.WasSuccess)
            {
                return PartialView(ViewNames.EditingRow, new EditingRowModel() { RouletteTypes = rouletteTypes.Value });
            }

            return StatusCode(rouletteTypes.StatusCode);
        }

        return StatusCode(result.StatusCode);
    }

    [HttpGet]
    public async Task<IActionResult> CancelUpdate(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetInstanceById(id), cancellationToken);

        if (result.WasSuccess)
        {
            return PartialView(ViewNames.EditableRow, result.Value);
        }

        return StatusCode(result.StatusCode);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteInstance(id), cancellationToken);

        if (result.WasSuccess)
        {
            return Ok();
        }

        return StatusCode(result.StatusCode);
    }
}
