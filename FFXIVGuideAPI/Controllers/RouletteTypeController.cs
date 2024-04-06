using FFXIVGuideAPI.Models.RouletteType.Commands;
using FluentResults.Extensions.AspNetCore;
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

    [HttpPost]
    public async Task<IActionResult> Create(CreateRouletteType request)
    {
        return (await _mediator.Send(request)).ToActionResult();
    }
}
