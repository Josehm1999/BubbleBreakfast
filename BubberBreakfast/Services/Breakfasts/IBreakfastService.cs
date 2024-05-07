using BubberBreakfast.Models;
using ErrorOr;

namespace BubberBreakfast.Services.Breakfasts;

public interface IBreakfastService
{
    Task<Guid> CreateBreakfast(Breakfast breakfast);
    Task<ErrorOr<Breakfast>> GetBreakfast(Guid id);
    Task DeleteBreakfast(Guid id);
    Task UpsertBreakfast(Breakfast breakfast);
}
