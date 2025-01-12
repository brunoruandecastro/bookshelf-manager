using bookshelf_manager.Components;
using MongoDB.Driver;
using BookshelfManager.Repositories;
using Users.brunoruandecastro.Documents.Desenvolvimento.bookshelf_manager.bookshelf_manager.Components;

public class Program
{
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                // Configuração MongoDB
                var mongoConnectionString = hostContext.Configuration.GetConnectionString("MongoDb");
                services.AddSingleton<IMongoClient>(new MongoClient(mongoConnectionString));
                services.AddScoped<IMongoDatabase>(sp =>
                {
                    var client = sp.GetRequiredService<IMongoClient>();
                    return client.GetDatabase("LibraryDb");
                });

                services.AddScoped<BookRepository>();
            });

    public static void Main(string[] args)
    {
        // Inicia o host para configuração dos serviços
        var host = CreateHostBuilder(args).Build();

        // Inicia a aplicação web com Razor Components
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
