using BubberBreakfast.Models;
using BubberBreakfast.ServiceErros;
using ErrorOr;
using Postgrest;

namespace BubberBreakfast.Services.Breakfasts;

public class BreakFastService : IBreakfastService
{

    Supabase.Client _client;

    public BreakFastService(Supabase.Client client)
    {
        _client = client;
    }

    public async Task<ErrorOr<Created>> CreateBreakfast(Breakfast breakfast)
    {
        await _client.From<Breakfast>().Insert(breakfast);
        return Result.Created;
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

    public async Task<ErrorOr<Deleted>> DeleteBreakfast(Guid id)
    {
        await _client.From<Breakfast>()
                     .Where(b => b.Id == id)
                     .Delete();

        return Result.Deleted;
    }

    public async Task<ErrorOr<UpsertBreakfastResult>> UpsertBreakfast(Breakfast breakfast)
    {
        var response = await _client.From<Breakfast>()
                                    .Upsert(breakfast, new QueryOptions { Count = QueryOptions.CountType.Exact });
        var isNewlyCreated = response.ResponseMessage.StatusCode == System.Net.HttpStatusCode.Created ? true : false;


        return new UpsertBreakfastResult(isNewlyCreated);
    }
}
