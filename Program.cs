using Microsoft.EntityFrameworkCore;
using ProgettoApi;
using ProgettoApi.models; // assicurati che il namespace sia corretto
using ProgettoApi.Service; // se hai servizi

var builder = WebApplication.CreateBuilder(args);

// Registrazione servizi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connessione al database via DbContext
builder.Services.AddDbContext<ParkingDbContext>();

builder.Services.AddScoped<IParkingService, ParkingService>();

var app = builder.Build();

// ✅ Applica le migrazioni DOPO app = builder.Build()
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ParkingDbContext>();
    db.Database.Migrate();
}

// Configurazione pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
