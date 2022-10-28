using Academia.MapeoDatos;
using Academia.MapeoDatos.Entidades;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Conexión Base de datos
builder.Services.AddDbContext<BaseDatosContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConectionBDAcademia"));
});

var app = builder.Build();

//migración
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BaseDatosContext>();
    context.Database.Migrate();

    //llenamos catálogos
    var afinidades = new List<Afinidad>();
    afinidades.Add(new Afinidad(){ Nombre = "Oscuridad"});
    afinidades.Add(new Afinidad(){ Nombre = "Luz" });
    afinidades.Add(new Afinidad(){ Nombre = "Fuego" });
    afinidades.Add(new Afinidad(){ Nombre = "Agua" });
    afinidades.Add(new Afinidad(){ Nombre = "Viento" });
    afinidades.Add(new Afinidad(){ Nombre = "Tierra" });
    context.Afinidad.AddRange(afinidades);

    var grimorios = new List<Grimorio>();
    grimorios.Add(new Grimorio() { Nombre = "Sinceridad", NumeroHojas = 1 });
    grimorios.Add(new Grimorio() { Nombre = "Esperanza", NumeroHojas = 2 });
    grimorios.Add(new Grimorio() { Nombre = "Amor", NumeroHojas = 3 });
    grimorios.Add(new Grimorio() { Nombre = "Buena Fortuna", NumeroHojas = 4 });
    grimorios.Add(new Grimorio() { Nombre = "Desesperación", NumeroHojas = 5 });
    context.Grimonio.AddRange(grimorios);

    context.SaveChanges();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
