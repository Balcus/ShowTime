using Microsoft.EntityFrameworkCore;
using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.Services;
using ShowTime.Components;
using ShowTime.DataAccess;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories;
using ShowTime.DataAccess.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ShowTimeDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IRepository<User>, UserRepository>();
builder.Services.AddTransient<IFestivalRepository, FestivalRepository>();
builder.Services.AddTransient<IRepository<Artist>, BaseRepository<Artist>>();
builder.Services.AddTransient<IRepository<Lineup>, BaseRepository<Lineup>>();
builder.Services.AddTransient<IRepository<Booking>, BaseRepository<Booking>>();

builder.Services.AddTransient<IArtistService, ArtistService>();
builder.Services.AddTransient<IFestivalService, FestivalService>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();