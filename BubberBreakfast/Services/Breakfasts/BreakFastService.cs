using BubberBreakfast.Models;
using BubberBreakfast.ServiceErros;
using ErrorOr;

namespace BubberBreakfast.Services.Breakfasts;

public class BreakFastService : IBreakfastService
{

    Supabase.Client _client;

    public BreakFastService(Supabase.Client client)
    {
        _client = client;
    }

    public async Task<Guid> CreateBreakfast(Breakfast breakfast)
    {
        var response = await _client.From<Breakfast>().Insert(breakfast);
        var newBreakfast = response.Models.First();
        return newBreakfast.Id;
    }

    public async Task<ErrorOr<Breakfast>> GetBreakfast(Guid id)
    {

        var response = await _client.From<Breakfast>()
            .Where(n => n.Id == id)
            .Get();

        if (response.Model is null)
        {
            return Errors.Breakfast.Notfound;
        }

        return response.Model;
    }

    public async Task DeleteBreakfast(Guid id)
    {
        await _client.From<Breakfast>()
                     .Where(b => b.Id == id)
                     .Delete();
    }

    public async Task UpsertBreakfast(Breakfast breakfast)
    {
        await _client.From<Breakfast>()
                     .Upsert(breakfast);
    }
}
