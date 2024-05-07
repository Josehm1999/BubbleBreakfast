using Microsoft.AspNetCore.Mvc;
using BubberBreakfast.Contracts.Breakfast;
using BubberBreakfast.Models;
using BubberBreakfast.Services.Breakfasts;
using ErrorOr;
using BubberBreakfast.ServiceErros;

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
        var response = new BreakfastResponse(
            breakfastId,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastTimeModified,
            breakfast.Savory,
            breakfast.Sweet);
        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new { id = breakfastId },
            value: response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBreakfast(Guid id)
    {
        ErrorOr<Breakfast> getBreakfastResult = await _breakfastService.GetBreakfast(id);

        if (getBreakfastResult.IsError && getBreakfastResult.FirstError == Errors.Breakfast.Notfound)
        {
            return NotFound();
        }
        var breakfast = getBreakfastResult.Value;

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
    public async Task<IActionResult> UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
    {
        var breakfast = new Breakfast()
        {
            Id = id,
            Name = request.Name,
            Description = request.Description,
            StartDateTime = request.StartDateTime,
            EndDateTime = request.EndDateTime,
            LastTimeModified = DateTime.UtcNow,
            Savory = request.Savory,
            Sweet = request.Sweet,
        };

        await _breakfastService.UpsertBreakfast(breakfast);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBreakfast(Guid id)
    {
        await _breakfastService.DeleteBreakfast(id);
        return NoContent();
    }
}
