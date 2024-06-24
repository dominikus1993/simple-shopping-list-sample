using Akka.Hosting;
using Akka.Routing;

using FastEndpoints;
using FastEndpoints.Swagger;

using ShoppingListSample.Core.Actors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddFastEndpoints();
builder.Services.AddAkka("ShoppingListSample", configurationBuilder =>
{
    configurationBuilder.WithActors((system, registry) =>
    {
        var actor = system.ActorOf(ShoppingListsActor.Props(), "shoppingLists");
        registry.Register<ShoppingListsActor>(actor);
    });
});
builder.Services.AddSwaggerDocument();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();
app.UseFastEndpoints();
app.UseSwaggerGen();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.MapFallbackToFile("index.html");

app.Run();

public partial class Program;