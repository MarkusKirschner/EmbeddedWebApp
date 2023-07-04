using System.Reflection;
using DxBlazorServerApp1.Data;

namespace DxBlazorServerApp1;

public static class AssemblyInitializer
{
    public static void Main()
    {
        // Required as an entry point for the application.
    }

    public static void RunServer()
    {
        try
        {
            Console.WriteLine("Current directory: " + AppDomain.CurrentDomain.BaseDirectory);
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions
            {
                ApplicationName = "DxBlazorServerApp1",
                ContentRootPath = AppDomain.CurrentDomain.BaseDirectory,
                EnvironmentName = Environments.Development,
                WebRootPath = "wwwroot"
            });
       
            builder.WebHost.UseUrls("http://localhost:5050");

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddDevExpressBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.Configure<DevExpress.Blazor.Configuration.GlobalOptions>(options =>
            {
                options.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v5;
            });
            builder.WebHost.UseWebRoot("wwwroot");
            builder.WebHost.UseStaticWebAssets();

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