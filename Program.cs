using EstoqueAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization; // garante que o ReferenceHandler esteja disponível

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        x.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

app.MapControllers();
app.Run();
