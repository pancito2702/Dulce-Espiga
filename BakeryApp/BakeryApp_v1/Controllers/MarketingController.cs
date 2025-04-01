using BakeryApp_v1.Models;
using BakeryApp_v1.Services;
using BakeryApp_v1.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BakeryApp_v1.Controllers
{
    [Authorize(Policy = "SoloAdministradores")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class MarketingController : Controller
    {
        private readonly BoletinService boletinService;
        private readonly MensajeBoletinService mensajeBoletinService;
        private readonly IMailEnviar mailEnviar;
        public MarketingController(BoletinService boletinService, MensajeBoletinService mensajeBoletinService, IMailEnviar mailEnviar)
        {
            this.boletinService = boletinService;
            this.mensajeBoletinService = mensajeBoletinService;
            this.mailEnviar = mailEnviar;
        }

        public async Task<IActionResult> EnviarCorreo([FromQuery] int idBoletin)
        {
            Boletin boletinBuscado = await boletinService.ObtenerBoletinPorId(idBoletin);

            if (boletinBuscado == null)
            {
                return NotFound();
            }

            return View();
        }

        public IActionResult EnviarCorreoTodos([FromQuery] int idBoletin)
        {

            return View();
        }


        public async Task<IActionResult> Marketing([FromQuery] int pagina)
        {
            int totalPaginas = await boletinService.CalcularTotalPaginas();

            if (pagina > totalPaginas)
            {
                return NotFound();
            }

            return View();
        }


        [HttpGet("/Marketing/ObtenerTodosLosUsuariosBoletin/{pagina}")]
        public async Task<IActionResult> ObtenerTodosLosUsuariosBoletin(int pagina)
        {
            //Si se intenta acceder por URL y por accidente se pone la pagina 0,
            //para que la aplicacion no se caiga
            if (pagina <= 0)
            {
                return BadRequest();
            }

            return new JsonResult(new { arregloBoletin = await boletinService.ObtenerUsuariosBoletinPorPagina(pagina) });
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerTotalPaginas()
        {
            int totalPaginas = await boletinService.CalcularTotalPaginas();
            return new JsonResult(new { paginas = totalPaginas });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]


        public async Task<IActionResult> GuardarMensajeBoletinTodos([FromBody] Mensajesboletin mensaje)
        {
            try
            {
              
                if (!mensajeBoletinService.VerificarDatosVaciosONulos(mensaje))
                {
                    return new JsonResult(new { mensaje = "Hay datos vacios revise", correcto = false });

                }

                IEnumerable<Boletin> todosLosUsuariosBoletines = await boletinService.ObtenerBoletinTodosLosUsuarios();

                bool resultadoEnviarCorreo = await mailEnviar.EnviarCorreoMarketingTodos(todosLosUsuariosBoletines, mensaje);


                if (!resultadoEnviarCorreo)
                {
                    return new JsonResult(new { mensaje = "Ha ocurrido un error al enviar el correo con el mensaje", correcto = false });
                }

                foreach (Boletin boletin in todosLosUsuariosBoletines)
                {
                    Mensajesboletin nuevoMensaje = new Mensajesboletin
                    {
                        Asunto = mensaje.Asunto,
                        Mensaje = mensaje.Mensaje,
                        IdBoletin = boletin.IdBoletin,
                    };

                    await mensajeBoletinService.Guardar(nuevoMensaje);
                }

                return new JsonResult(new { mensaje = Url.Action("Marketing", "Marketing"), correcto = true, mensajeInfo = "Mensaje enviado con exito a todos" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al enviar el mensaje", correcto = false });
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]


        public async Task<IActionResult> GuardarMensajeBoletin([FromBody]Mensajesboletin mensaje)
        {
            try
            {
                Boletin boletinUsuario = await boletinService.ObtenerBoletinPorId(mensaje.IdBoletin);
                if (!mensajeBoletinService.VerificarDatosVaciosONulos(mensaje))
                {
                    return new JsonResult(new { mensaje = "Hay datos vacios revise", correcto = false });

                }

                bool resultadoEnviarCorreo = await mailEnviar.EnviarCorreoMarketingPersona(boletinUsuario, mensaje);


                if (!resultadoEnviarCorreo)
                {
                    return new JsonResult(new { mensaje = "Ha ocurrido un error al enviar el correo con el mensaje", correcto = false });
                }

                await mensajeBoletinService.Guardar(mensaje);
                return new JsonResult(new { mensaje = Url.Action("Marketing", "Marketing"), correcto = true, mensajeInfo = "Mensaje enviado con exito" });
            }
            catch (Exception ex)
            {
               return new JsonResult(new { mensaje = "Ha ocurrido un error al enviar el mensaje", correcto = false });
            }
        }
    }
}
