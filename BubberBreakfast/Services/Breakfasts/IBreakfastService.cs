using BubberBreakfast.Models;
using ErrorOr;

namespace BubberBreakfast.Services.Breakfasts;

public interface IBreakfastService
{
    Task<ErrorOr<Created>> CreateBreakfast(Breakfast breakfast);
    Task<ErrorOr<Breakfast>> GetBreakfast(Guid id);
    Task<ErrorOr<Deleted>> DeleteBreakfast(Guid id);
    Task<ErrorOr<UpsertBreakfastResult>> UpsertBreakfast(Breakfast breakfast);
}
