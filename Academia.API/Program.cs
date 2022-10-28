using Academia.MapeoDatos;
using Academia.MapeoDatos.Entidades;
using Academia.Negocio;
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

builder.Services.AddScoped(typeof(SolicitudNegocio));

var app = builder.Build();

//migración
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BaseDatosContext>();
    context.Database.Migrate();

    ////llenamos catálogos
    var afinidades = new List<Afinidad>()
    {
        new Afinidad() { Nombre = "Oscuridad" },
        new Afinidad() { Nombre = "Luz" },
        new Afinidad() { Nombre = "Fuego" },
        new Afinidad() { Nombre = "Agua" },
        new Afinidad() { Nombre = "Viento" },
        new Afinidad() { Nombre = "Tierra" }
    };
    afinidades.ForEach( nvoAfinidad => {
        var existe = context.Afinidad.Where(w => w.Nombre.Contains(nvoAfinidad.Nombre)).Any();
        if (!existe)
        {
            context.Afinidad.Add(nvoAfinidad);
        }
    });

    var grimorios = new List<Grimorio>()
    {
        new Grimorio() { Nombre = "Sinceridad", NumeroHojas = 1 },
        new Grimorio() { Nombre = "Esperanza", NumeroHojas = 2 },
        new Grimorio() { Nombre = "Amor", NumeroHojas = 3 },
        new Grimorio() { Nombre = "Buena Fortuna", NumeroHojas = 4 },
        new Grimorio() { Nombre = "Desesperación", NumeroHojas = 5 }
    };
    grimorios.ForEach( nvoGrimorio => {
        var existe = context.Grimonio.Where(w => w.Nombre.Contains(nvoGrimorio.Nombre)).Any();
        if (!existe)
        {
            context.Grimonio.Add(nvoGrimorio);
        }
    });


    var estatus = new List<Estatus>()
    {
        new Estatus { Nombre = "Pendiente" },
        new Estatus { Nombre = "Aceptado" },
        new Estatus { Nombre = "Rechazado" }
    };
    estatus.ForEach(nvoEstatus => {
        var existe = context.Estatus.Where(w => w.Nombre.Contains(nvoEstatus.Nombre)).Any();
        if (!existe)
        {
            context.Estatus.AddRange(estatus);
        }
    });    

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
