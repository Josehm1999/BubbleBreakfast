using System.Xml;
using System.Xml.Serialization;
using BubberBreakfast.Models;
using BubberBreakfast.ServiceErros;
using ErrorOr;
using Postgrest;
using UBL21;

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

    public void invoiceTypeExample()
    {
        InvoiceType invoiceType = new InvoiceType();
        UBLVersionIDType uBLVersionIDType = new UBLVersionIDType();

        uBLVersionIDType.Value = "2.1";
        invoiceType.UBLVersionID = uBLVersionIDType;

        XmlSerializer xmlSerializer = new(typeof(InvoiceType));
        var oStringWriter = new StringWriter();
        xmlSerializer.Serialize(XmlWriter.Create(oStringWriter), invoiceType);
        string stringXML = oStringWriter.ToString();

        System.IO.File.WriteAllText("XML_Sunat", stringXML);
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
