using Microsoft.AspNetCore.Mvc;
using BubberBreakfast.Contracts.Breakfast;
using BubberBreakfast.Models;
using BubberBreakfast.Services.Breakfasts;
using ErrorOr;
using BubberBreakfast.ServiceErros;
using UBL21;

namespace BubberBreakfast.Controllers;

public class BreakfastsController : ApiController
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

        ErrorOr<Created> createBreakfastResult = await _breakfastService.CreateBreakfast(breakfast);


        if (createBreakfastResult.IsError)
        {
            return Problem(createBreakfastResult.Errors);
        }

        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new { id = breakfast.Id },
            value: MapBreakfastResponse(breakfast)
        );
    }

    private static BreakfastResponse MapBreakfastResponse(Breakfast breakfast)
    {
        return new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastTimeModified,
            breakfast.Savory,
            breakfast.Sweet);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBreakfast(Guid id)
    {
        ErrorOr<Breakfast> getBreakfastResult = await _breakfastService.GetBreakfast(id);

        return getBreakfastResult.Match(
                    breakfast => Ok(MapBreakfastResponse(breakfast)),
                    errors => Problem(errors)
                    );
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

       ErrorOr<Updated> updatedResult = await _breakfastService.UpsertBreakfast(breakfast);
       // updatedResult.Match(
       //         updated =>
       //         )
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBreakfast(Guid id)
    {
        ErrorOr<Deleted> deletedResult = await _breakfastService.DeleteBreakfast(id);

        return deletedResult.Match(
                deleted => NoContent(),
                errors => Problem(errors)
        );
    }
}
