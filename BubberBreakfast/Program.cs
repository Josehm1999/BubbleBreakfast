using BubberBreakfast.Services.Breakfasts;
using Supabase;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddSingleton<Supabase.Client>(_ =>
            new Supabase.Client(
                builder.Configuration["Supabaseurl"],
                builder.Configuration["Supabasekey"],
                new SupabaseOptions
                {
                    AutoRefreshToken = true,
                    AutoConnectRealtime = true
                }));
    builder.Services.AddSingleton<IBreakfastService, BreakFastService>();
}



var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();

    // app.UseAuthorization();

    app.MapControllers();

    app.Run();
}

