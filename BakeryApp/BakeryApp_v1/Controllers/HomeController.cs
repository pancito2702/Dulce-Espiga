using BakeryApp_v1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using BakeryApp_v1.Services;
using BakeryApp_v1.Utilidades;
using BakeryApp_v1.DTO;
using System.Threading;
using BakeryApp_v1.ViewModels;
using System.Security.Principal;

namespace BakeryApp_v1.Controllers
{
    public class HomeController : Controller
    {
        private readonly PersonaService personaService;
        private readonly IFuncionesUtiles funcionesUtiles;
        private readonly IMailEnviar mailEnviar;
        private readonly ReestablecerContraService reestablecerContraService;
        private readonly CategoriaService categoriaService; 
        private readonly ProductoService productoService;
        private readonly ProductoPedidoService productoPedidoService;

        public HomeController(PersonaService personaService, IFuncionesUtiles funcionesUtiles, IMailEnviar mailEnviar, ReestablecerContraService reestablecerContraService, CategoriaService categoriaService, ProductoService productoService, ProductoPedidoService productoPedidoService)
        {
            this.personaService = personaService;
            this.funcionesUtiles = funcionesUtiles;
            this.mailEnviar = mailEnviar;
            this.reestablecerContraService = reestablecerContraService;
            this.categoriaService = categoriaService;
            this.productoService = productoService;
            this.productoPedidoService = productoPedidoService;
        }



        public async Task<IActionResult> Tienda([FromQuery] int pagina)
        {
            int totalPaginas = await categoriaService.CalcularTotalPaginas();
            if (pagina > totalPaginas)
            {
                return NotFound();
            }


            return View();
        }

        public async Task<IActionResult> ProductosPorCategoria([FromQuery] int categoria)
        {
            Categoria categoriaBuscada = await categoriaService.ObtenerCategoriaPorId(categoria);

            if (categoriaBuscada == null)
            {
                return NotFound();
            }


            return View();
        }


        [HttpGet]

        public async Task<IActionResult> ObtenerProductosMasVendidos()
        {
            return new JsonResult(new { arregloProductosMasVendidos = await productoPedidoService.ObtenerProductosMasVendidos() });
        }



        [HttpGet("/Home/ObtenerProductosPorCategoria/{idCategoria}")]

        public async Task<IActionResult> ObtenerProductosPorCategoria(int idCategoria)
        {
            return new JsonResult(new { arregloProductos = await productoService.ObtenerTodasLasProductosPorCategoria(idCategoria) });
        }

        [HttpGet("/Home/ObtenerCategoriaPorId/{idCategoria}")]

        public async Task<IActionResult> ObtenerCategoriaPorId(int idCategoria)
        {
            return new JsonResult(new { categoria = await categoriaService.ObtenerCategoriaPorId(idCategoria) });
        }

        [HttpGet]

        public async Task<IActionResult> ObtenerTotalPaginas(int pagina)
        {


            return new JsonResult(new { paginas = await categoriaService.CalcularTotalPaginas() });
        }

        [HttpGet("/Home/ObtenerCategorias/{pagina}")]

        public async Task<IActionResult> ObtenerCategorias(int pagina)
        {
            if (pagina <= 0)
            {
                return NotFound();
            }

            return new JsonResult(new { arregloCategorias = await categoriaService.ObtenerTodasLasCategorias(pagina) });
        }

        public IActionResult Index()
        {
            return View();
        }

     
        public IActionResult AccesoDenegado()
        {
            return View();

        }

      

        public IActionResult PersonalizarProducto()
        {
            return View();
        }

        public IActionResult Servicios()
        {
            return View();
        }

        public IActionResult SobreNosotros()
        {
            return View();
        }

        public IActionResult Contacto()
        {
            return View();
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

      
       


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IniciarSesion([FromBody] Persona persona)
        {
            try
            {
                if (!personaService.VerificarCorreoOContraVacia(persona))
                {
                    return new JsonResult(new { mensaje = "Hay datos vacios por favor revise", correcto = false});
                }


                Persona personaEncontrada = await personaService.ObtenerPersonaPorCorreo(persona);
                if (personaEncontrada == null)
                {
                    return new JsonResult(new { mensaje = "Correo o contraseña incorrectos", correcto = false });
                }

                if (!personaService.VerificarContraConContraUsuario(persona, personaEncontrada))
                {
                    return new JsonResult(new { mensaje = "Correo o contraseña incorrectos", correcto = false });
                }


                PersonaDTO personaConRoles = await personaService.ObtenerPersonaConRoles(persona);

                List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.Email,personaConRoles.Correo),
                    new Claim(ClaimTypes.Role,personaConRoles.Rol.NombreRol),
                    new Claim(ClaimTypes.Sid,personaConRoles.IdPersona.ToString())
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true
                };
            

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);


                if (personaConRoles.Rol.NombreRol == "ADMINISTRADOR")
                {
                    return new JsonResult(new { mensaje = Url.Action("Index", "Administrador"), correcto = true });
                }

                if (personaConRoles.Rol.NombreRol == "EMPLEADO")
                {
                    return new JsonResult(new { mensaje = Url.Action("Index", "Empleado"), correcto = true });
                }

                if (personaConRoles.Rol.NombreRol == "CLIENTE")
                {
                    return new JsonResult(new { mensaje = Url.Action("Index", "UsuarioRegistrado"), correcto = true });
                }
               
                return new JsonResult(new { mensaje = "Error: Rol incorrecto", correcto = false });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error", correcto = false });
            }
        }

        public IActionResult Registrarse()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrarse([FromBody] Persona persona)
        {
            try
            {

                if (personaService.VerificarDatosVaciosONulos(persona))
                {
                    return new JsonResult(new { mensaje = "Hay datos vacios, por favor revise", correcto = false});
                }



                bool resultadoRepetidaCorreo = await personaService.VerificarCorreoRepetido(persona);

                if (resultadoRepetidaCorreo)
                {
                    return new JsonResult(new { mensaje = "El correo de la persona ya se encuentra registrado", correcto = false });
                }


                bool resultadoRepetidaTelefono = await personaService.VerificarTelefonoRepetido(persona);

                if (resultadoRepetidaTelefono)
                {
                    return new JsonResult(new { mensaje = "El telefono de la persona ya se encuentra registrado", correcto = false });
                }


                if (!personaService.ValidarLongitudContra(persona))
                {
                    return new JsonResult(new { mensaje = "La contraseña debe ser mayor a 8 caracteres", correcto = false });
                }


                if (!personaService.ValidarLongitudTelefono(persona))
                {
                    return new JsonResult(new { mensaje = "La longitud del telefono debe ser igual a 8 caracteres, con un guion", correcto = false });
                }

                if (!personaService.ValidarNumeroTelefono(persona))
                {
                    return new JsonResult(new { mensaje = "El numero de telefono no es valido", correcto = false });
                }

                if (!personaService.VerificarCorreoElectronico(persona))
                {
                    return new JsonResult(new { mensaje = "El correo electronico no es valido", correcto = false });
                }


                if (funcionesUtiles.EncriptarContra(persona) == null)
                {
                    return new JsonResult(new { mensaje = "Ha sucedido un error al encriptar la contrase�a", correcto = false });
                }

                persona.IdRol = 3;

                if (!personaService.VerificarRolPersona(persona))
                {
                    return new JsonResult(new { mensaje = "Ha ocurrido un error", correcto = false });
                }


                await personaService.Guardar(persona);
                return new JsonResult(new { mensaje = Url.Action("IniciarSesion", "Home"), correcto = true, mensajeInfo = "Persona registrada con exito"});
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al guardar la persona", correcto = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CerrarSesion()
        {


            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }



        public IActionResult RecuperarContrasena()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EnviarCorreoContra([FromBody] Persona persona)
        {
            try
            {


                if (personaService.VerificarCorreoVacio(persona))
                {
                    return new JsonResult(new { mensaje = "El correo no puede estar vacio", correcto = false });
                }


                Persona personaConContraOlvidada = await personaService.ObtenerPersonaPorCorreo(persona);

                if (personaConContraOlvidada == null)
                {
                    return new JsonResult(new { mensaje = "Correo electronico no encontrado", correcto = false });
                }

                Recuperarcontra verificacionesPersona = reestablecerContraService.ConvertirPersonaARecuperarPersona(personaConContraOlvidada);



                Recuperarcontra contraABorrar = await reestablecerContraService.ObtenerPorIdPersona(verificacionesPersona);

                if (contraABorrar is not null)
                {
                    await reestablecerContraService.Eliminar(contraABorrar);
                }


                string codigoRecuperacion = funcionesUtiles.GenerarGUID();

                bool envioCorrecto = await mailEnviar.EnviarCorreo(personaConContraOlvidada, "Código de recuperación para la contraseña olvidada", codigoRecuperacion);

                if (!envioCorrecto)
                {
                    return new JsonResult(new { mensaje = "Ha ocurrido un error al enviar el correo", correcto = false });
                }


                Recuperarcontra personaContra = reestablecerContraService.ConvertirPersonaARecuperarPersona(personaConContraOlvidada, codigoRecuperacion);

                await reestablecerContraService.Guardar(personaContra);

                return new JsonResult(new { mensaje = Url.Action("CambiarContrasena", "Home"), mensajeInfo = "Por favor revise la bandeja de entrada de su correo electronico", correcto = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error inesperado", correcto = false });
            }
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarContra([FromBody] ReestablecerPersonaViewModel persona)
        {
            try
            {

                Persona personaNormal = ReestablecerPersonaViewModel.ConvertirReestablecerPersonaViewModelAPersona(persona);

                if (!personaService.VerificarCorreoOContraVacia(personaNormal))
                {
                    return new JsonResult(new { mensaje = "Correo o contraseña vacias", correcto = false });
                }

                if (!personaService.ValidarLongitudContra(personaNormal))
                {
                    return new JsonResult(new { mensaje = "La contraseña debe ser mayor a 8 caracteres", correcto = false });
                }


                personaNormal = await personaService.ObtenerPersonaPorCorreo(personaNormal);

                if (personaNormal == null)
                {
                    return new JsonResult(new { mensaje = "Correo electronico no encontrado", correcto = false });
                }
                // Se crea y asigna el objeto verificacionesPersona, con el codigo de recuperacion enviado desde el frontend
                Recuperarcontra verificacionesPersona = reestablecerContraService.ConvertirPersonaARecuperarPersona(personaNormal, persona.CodigoRecuperacion);

                if (reestablecerContraService.EsVacioCodigo(verificacionesPersona))
                {

                    return new JsonResult(new { mensaje = "El codigo de recuperación no puede estar vacio", correcto = false });
                }

                bool estaExpirado = await reestablecerContraService.VerificarFechaCodigo(verificacionesPersona);


                if (estaExpirado)
                {
                    Recuperarcontra contraABorrar = await reestablecerContraService.ObtenerPorIdPersona(verificacionesPersona);
                    await reestablecerContraService.Eliminar(contraABorrar);
                    return new JsonResult(new { mensaje = "El codigo de recuperación esta expirado, por favor repita el proceso", correcto = false });
                }

                Recuperarcontra personaCodigoActual = await reestablecerContraService.ObtenerPorIdPersona(verificacionesPersona);

           

                // Si el codigo es null, se retorna un mensaje de error
                if (personaCodigoActual == null)
                {
                    return new JsonResult(new { mensaje = "El codigo de recuperacion es incorrecto", correcto = false });
                }

                // Se compara el codigo de la base de datos, con el codigo enviado desde el frontend
                if (personaCodigoActual.CodigoRecuperacion != verificacionesPersona.CodigoRecuperacion)
                {
                    return new JsonResult(new { mensaje = "El codigo de recuperacion es incorrecto", correcto = false });
                }

                // Se asigna la contrase�a a la contrase�a que viene de la vista
                personaNormal.Contra = persona.Contra;


                if (funcionesUtiles.EncriptarContra(personaNormal) == null)
                {
                    return new JsonResult(new { mensaje = "Ha sucedido un error al encriptar la contraseña", correcto = false });
                }

                await personaService.Editar(personaNormal);
                await reestablecerContraService.Eliminar(personaCodigoActual);

                return new JsonResult(new {mensajeInfo = "Contraseña reestablecida con exito", mensaje = Url.Action("IniciarSesion", "Home"), correcto = true});
            }
            catch (Exception ex)
            {

                return new JsonResult(new { mensaje = "Ha ocurrido un error inesperado" });
            }
        }



        public IActionResult CambiarContrasena()
        {
            return View();
        }

        public IActionResult Carro()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
