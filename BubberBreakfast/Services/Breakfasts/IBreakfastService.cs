using BubberBreakfast.Models;

namespace BubberBreakfast.Services.Breakfasts;

public interface IBreakfastService
{
    Task<Guid> CreateBreakfast(Breakfast breakfast);
    Task<Breakfast> GetBreakfast(Guid id);
}
