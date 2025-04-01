using BakeryApp_v1.Models;
using BakeryApp_v1.Services;
using BakeryApp_v1.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BakeryApp_v1.Controllers
{
    [Authorize(Policy = "SoloAdministradores")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class PerfilController : Controller
    {
        private readonly PersonaService personaService;
        private readonly RolService rolService;
        private readonly IFuncionesUtiles funcionesUtiles;
        public PerfilController(PersonaService personaService, RolService rolService, IFuncionesUtiles funcionesUtiles)
        {
            this.rolService = rolService;
            this.personaService = personaService;
            this.funcionesUtiles = funcionesUtiles;
        }


        public async Task<ActionResult> Index([FromQuery] int pagina)
        {
            int totalPaginas = await personaService.CalcularTotalPaginas();
            if (pagina > totalPaginas)
            {
                return NotFound();
            }


            return View();
        }

        public ActionResult CrearPerfil()
        {
            return View();
        }

        public async Task<IActionResult> EditarPerfil([FromQuery] int idPersona)
        {
            Persona personaEditar = await personaService.ObtenerPersonaPorId(idPersona);

            if (personaEditar == null)
            {
                return NotFound();
            }


            return View();
        }


        [HttpGet("/Perfil/DevolverPerfilEspecifico/{idPersona}")]
        public async Task<JsonResult> DevolverPerfilEspecifico(int idPersona)
        {
            Persona personaEncontrada = await personaService.ObtenerPersonaPorId(idPersona);
            return new JsonResult(new { persona = personaEncontrada });
        }

        [HttpGet("/Perfil/ObtenerTodasLasPersonas/{pagina}")]
        public async Task<IActionResult> ObtenerTodasLasPersonas(int pagina)
        {
            if (pagina <= 0)
            {
                return BadRequest();
            }
            return new JsonResult(new { arregloPersonas = await personaService.ObtenerTodasLasPersonas(pagina) });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> GuardarPerfil([FromBody] Persona persona)
        {
            try
            {

                if (personaService.VerificarDatosVaciosONulos(persona))
                {
                    return new JsonResult(new { mensaje = "Hay datos vacios, por favor revise", correcto = false });
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
                    return new JsonResult(new { mensaje = "Ha sucedido un error al encriptar la contraseña", correcto = false });
                }

                await personaService.Guardar(persona);
                return new JsonResult(new { mensaje = Url.Action("Index", "Perfil"), correcto = true, mensajeInfo = "Persona guardada con exito" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al guardar la persona", correcto = false});
            }
        }


        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> GuardarEditado([FromBody] Persona persona)
        {
            try
            {

                if (personaService.VerificarDatosVaciosONulos(persona))
                {
                    return new JsonResult(new { mensaje = "Hay datos vacios, por favor revise" });
                }



                bool resultadoRepetidaCorreo = await personaService.VerificarCorreoRepetido(persona);

                if (resultadoRepetidaCorreo)
                {
                    return new JsonResult(new { mensaje = "El correo de la persona ya se encuentra registrado" });
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
                    return new JsonResult(new { mensaje = "Ha sucedido un error al encriptar la contraseña", correcto = false });
                }

                await personaService.Editar(persona);
                return new JsonResult(new { mensaje = Url.Action("Index", "Perfil"), correcto = true, mensajeInfo = "Persona modificado con exito" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al modificar la persona", correcto = false });
            }
        }


        [HttpPost]

        public async Task<JsonResult> ObtenerTodosLosRoles()
        {
            return new JsonResult(new { arregloRoles = await rolService.ObtenerTodosLosRoles() });
        }

        [HttpGet]

        public async Task<JsonResult> ObtenerTotalPaginas()
        {
            return new JsonResult(new { paginas = await personaService.CalcularTotalPaginas() });
        }


        [HttpDelete("/Perfil/EliminarPerfil/{idPersona}")]
        [ValidateAntiForgeryToken]

        public async Task<JsonResult> EliminarPerfil(int idPersona)
        {
            try
            {
                Persona personaABorrar = await personaService.ObtenerPersonaPorId(idPersona);

                await personaService.Eliminar(personaABorrar);
                return new JsonResult(new { mensaje = "Perfil borrado con exito", correcto = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al eliminar el perfil de la persona", correcto = false });
            }
        }
    }
}
