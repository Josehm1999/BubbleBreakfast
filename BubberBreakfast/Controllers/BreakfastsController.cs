using Microsoft.AspNetCore.Mvc;
using BubberBreakfast.Contracts.Breakfast;
using BubberBreakfast.Models;
using BubberBreakfast.Services.Breakfasts;

namespace BubberBreakfast.Controllers;

[ApiController]
[Route("[controller]")]
public class BreakfastsController : ControllerBase
{
    private readonly IBreakfastService _breakfastService;

    public BreakfastsController(IBreakfastService breakfastService)
    {
        _breakfastService = breakfastService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBreakfastAsync(CreateBreakfastRequest request)
    {

        var breakfast = new Breakfast()
        {
            Name = request.Name,
            Description = request.Description,
            StartDateTime = request.StartDateTime,
            EndDateTime = request.EndDateTime,
            LastTimeModified = DateTime.UtcNow,
            Savory = request.Savory,
            Sweet = request.Sweet
        };

        var breakfastId = await _breakfastService.CreateBreakfast(breakfast);
        Console.WriteLine(breakfastId);
        var response = new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastTimeModified,
            breakfast.Savory,
            breakfast.Sweet);
        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new { id = breakfast.Id},
            value: response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBreakfast(Guid id)
    {
        var breakfast = await _breakfastService.GetBreakfast(id);

        var response = new BreakfastResponse(
                breakfast.Id,
                breakfast.Name,
                breakfast.Description,
                breakfast.StartDateTime,
                breakfast.EndDateTime,
                breakfast.LastTimeModified,
                breakfast.Savory,
                breakfast.Sweet
                );

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
    {
        return Ok(request);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        return Ok(id);
    }
}
