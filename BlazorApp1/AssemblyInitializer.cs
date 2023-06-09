using BlazorApp1.Data;

namespace BlazorApp1;

public static class AssemblyInitializer
{
    private const string ServerUrl = "http://localhost:3045";

    public static void Main()
    {
        // Required as an entry point for the application.
    }

    public static void RunServer()
    {
        try
        {
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions
            {
                ApplicationName = "BlazorApp1", // Change Pages/_Host.cshtml:16, too
                ContentRootPath = AppDomain.CurrentDomain.BaseDirectory,
                EnvironmentName = Environments.Development,
                WebRootPath = "wwwroot"
            });
            builder.WebHost.UseUrls(ServerUrl);
// Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddDevExpressBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();

            var app = builder.Build();

// Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine($"Could not start server {exception}");
        }
    }
}