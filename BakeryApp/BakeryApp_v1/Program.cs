using BakeryApp_v1.DAO;
using BakeryApp_v1.Models;
using BakeryApp_v1.Services;
using BakeryApp_v1.Utilidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.StaticFiles;
using Stripe;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();



builder.Services.AddDbContext<BakeryAppContext>(options =>
options.UseMySQL(builder.Configuration.GetConnectionString("conexion")));


builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "RequestVerificationToken";
});


builder.Services.AddScoped<CategoriaDAO, CategoriaDAOImpl>();
builder.Services.AddScoped<CategoriaService, CategoriaServiceImpl>();
builder.Services.AddScoped<PersonaDAO, PersonaDAOImpl>();
builder.Services.AddScoped<PersonaService, PersonaServiceImpl>();
builder.Services.AddScoped<RolDAO, RolDAOImpl>();
builder.Services.AddScoped<RolService, RolServiceImpl>();
builder.Services.AddScoped<IngredienteDAO, IngredienteDAOImpl>();
builder.Services.AddScoped<IngredienteService, IngredienteServiceImpl>();
builder.Services.AddScoped<RecetaDAO, RecetaDAOImpl>();
builder.Services.AddScoped<RecetaService, RecetaServiceImpl>();
builder.Services.AddScoped<ProductoDAO, ProductoDAOImpl>();
builder.Services.AddScoped<ProductoService, ProductoServiceImpl>();
builder.Services.AddScoped<IFuncionesUtiles, FuncionesUtiles>();
builder.Services.AddScoped<UnidadMedidaDAO, UnidadMedidaDAOImpl>();
builder.Services.AddScoped<UnidadMedidaService, UnidadMedidaServiceImpl>();
builder.Services.AddScoped<ReestablecerContraDAO, ReestablecerContraDAOImpl>();
builder.Services.AddScoped<ReestablecerContraService, ReestablecerContraServiceImpl>();
builder.Services.AddScoped<ProvinciaDAO, ProvinciaDAOImpl>();
builder.Services.AddScoped<CantonDAO, CantonDAOImpl>();
builder.Services.AddScoped<DistritoDAO, DistritoDAOImpl>();
builder.Services.AddScoped<ProvinciaService, ProvinciaServiceImpl>();
builder.Services.AddScoped<CantonService, CantonServiceImpl>();
builder.Services.AddScoped<DistritoService, DistritoServiceImpl>();
builder.Services.AddScoped<DireccionesDAO, DireccionesDAOImpl>();
builder.Services.AddScoped<DireccionesService, DireccionesServiceImpl>();
builder.Services.AddScoped<IMailEnviar, MailEnviar>();
builder.Services.AddScoped<CarritoDAO, CarritoDAOImpl>();
builder.Services.AddScoped<CarritoService, CarritoServiceImpl>();
builder.Services.AddScoped<TiposPagoDAO, TiposDePagoDAOImpl>();
builder.Services.AddScoped<TiposEnvioDAO, TiposDeEnvioDAOImpl>();
builder.Services.AddScoped<EstadosPedidoDAO, EstadosPedidoDAOImpl>();
builder.Services.AddScoped<PedidoDAO, PedidoDAOImpl>();
builder.Services.AddScoped<PedidoService, PedidoServiceImpl>();
builder.Services.AddScoped<TiposDePagoService, TiposDePagoServiceImpl>();
builder.Services.AddScoped<TiposDeEnvioService, TiposDeEnvioServiceImpl>();
builder.Services.AddScoped<EstadosPedidoService, EstadosPedidoServiceImpl>();
builder.Services.AddScoped<PagoSinpeDAO, PagoSinpeDAOImpl>();
builder.Services.AddScoped<PagoSinpeService, PagoSinpeServiceImpl>();
builder.Services.AddScoped<ProductoPedidoDAO, ProductoPedidoDAOImpl>();
builder.Services.AddScoped<ProductoPedidoService, ProductoPedidoServiceImpl>();
builder.Services.AddScoped<FacturaDAO, FacturaDAOImpl>();
builder.Services.AddScoped<FacturaService, FacturaServiceImpl>();
builder.Services.AddScoped<LineaFacturaDAO, LineaFacturaDAOImpl>();
builder.Services.AddScoped<LineaFacturaService, LineaFacturaServiceImpl>();
builder.Services.AddScoped<BoletinDAO, BoletinDAOImpl>();
builder.Services.AddScoped<BoletinService, BoletinServiceImpl>();
builder.Services.AddScoped<BoletinDAO, BoletinDAOImpl>();
builder.Services.AddScoped<BoletinNoticiasDAO, BoletinNoticiasDAOImpl>();
builder.Services.AddScoped<BoletinNoticiasService, BoletinNoticiasServiceImpl>();
builder.Services.AddScoped<MensajesBoletinDAO, MensajeBoletinDAOImpl>();
builder.Services.AddScoped<MensajeBoletinService, MensajeBoletinServiceImpl>();
builder.Services.AddScoped<NotaCreditoDAO, NotaCreditoDAOImpl>();
builder.Services.AddScoped<NotaCreditoService, NotaCreditoServiceImpl>();






builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "UserDetails";
        options.LoginPath = "/Home/IniciarSesion";
        options.LogoutPath = "/Home/CerrarSesion";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.AccessDeniedPath = "/Home/AccesoDenegado";
    });

builder.Services.AddAuthorization(options =>
{

    options.AddPolicy("SoloAdministradores", policy =>
            policy.RequireRole("ADMINISTRADOR")
            );

    options.AddPolicy("SoloClientes", policy =>
         policy.RequireRole("CLIENTE")
         );

    options.AddPolicy("SoloEmpleados", policy =>
     policy.RequireRole("EMPLEADO")
     );
});

if (!builder.Environment.IsDevelopment())
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.Listen(System.Net.IPAddress.Loopback, 5001);
    });
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}


app.UseHttpsRedirection(); 


app.UseStaticFiles();



app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();




app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
