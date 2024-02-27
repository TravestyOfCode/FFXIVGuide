using FFXIVGuide.Web.Data.Encounter.Queries;
using FFXIVGuide.Web.Data.Instance.Queries;
using FFXIVGuide.Web.Data.RouletteType.Queries;
using FFXIVGuide.Web.Models.Guide;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FFXIVGuide.Web.Controllers;

public class GuideController : Controller
{
    private readonly IMediator _mediator;

    public GuideController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Index(GetAllRouletteTypesAsDict request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);

        if (result.WasSuccess)
        {
            var model = new IndexViewModel() { Sidebar = new SidebarViewModel() { RouletteTypeList = result.Value } };

            return View(model);
        }

        return StatusCode(result.StatusCode);
    }

    [HttpGet]
    public async Task<IActionResult> InstanceList(GetInstancesAsDictByRouletteTypeId request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);

        if (result.WasSuccess)
        {
            return PartialView(result.Value);
        }

        return StatusCode(result.StatusCode);
    }

    [HttpGet]
    public async Task<IActionResult> Search(GetInstancesAsDictByNameSearch request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);

        if (result.WasSuccess)
        {
            return PartialView(nameof(InstanceList), result.Value);
        }

        return StatusCode(result.StatusCode);
    }

    [HttpGet]
    public async Task<IActionResult> InstanceDetails(GetInstanceById request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);

        if (result.WasSuccess)
        {
            // Get the encounters for the instance
            var encRequest = new GetEncountersByInstanceId(request.Id, User.FindFirstValue(ClaimTypes.NameIdentifier));

            var encResult = await _mediator.Send(encRequest, cancellationToken);

            if (encResult.WasSuccess)
            {
                result.Value.Encounters = encResult.Value;

                return PartialView(result.Value);
            }

            return StatusCode(encResult.StatusCode);
        }

        return StatusCode(result.StatusCode);
    }
}
