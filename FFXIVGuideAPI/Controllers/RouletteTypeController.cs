using FFXIVGuideAPI.Data.Errors;
using FFXIVGuideAPI.Models.RouletteType.Commands;
using FFXIVGuideAPI.Models.RouletteType.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FFXIVGuideAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class RouletteTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public RouletteTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetRouletteTypes()
    {
        var result = await _mediator.Send(new GetRouletteTypes());

        return this.ToActionResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRouletteType(int id)
    {
        var result = await _mediator.Send(new GetRouletteTypeById(id));

        return this.ToActionResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRouletteType request)
    {
        var result = (await _mediator.Send(request)).AddErrors(ModelState);

        return this.ToActionResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateRouletteType request)
    {
        var result = (await _mediator.Send(request)).AddErrors(ModelState);

        return this.ToActionResult(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(DeleteRouletteType request)
    {
        var result = (await _mediator.Send(request)).AddErrors(ModelState);

        return this.ToActionResult(result);
    }
}
