using BakeryApp_v1.DAO;
using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
using BakeryApp_v1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BakeryApp_v1.Controllers
{

    [Authorize(Policy = "SoloAdministradores")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class FacturaAdminController : Controller
    {
        private readonly FacturaService facturaService;
        private readonly NotaCreditoService notaCreditoService;
        public FacturaAdminController(FacturaService facturaService, NotaCreditoService notaCreditoService)
        {
            this.facturaService = facturaService;
            this.notaCreditoService = notaCreditoService;
        }

        public async Task<IActionResult> VerFactura([FromQuery] int idFactura)
        {
            Factura facturaBuscada = await facturaService.ObtenerPorIdFacturaNormal(idFactura);

            if (facturaBuscada == null)
            {
                return NotFound();
            }
            return View();
        }

        public async Task<IActionResult> VerNotaCredito([FromQuery] int idFactura)
        {
            Notascredito notaCreditoBuscada = await notaCreditoService.ObtenerNotaCreditoPorIdFactura(idFactura);

            if (notaCreditoBuscada == null)
            {
                return NotFound();
            }
            return View();
        }



        public async Task<IActionResult> Facturas([FromQuery] int pagina)
        {

            int totalPaginas = await facturaService.CalcularTotalPaginas();
            if (pagina > totalPaginas)
            {
                return NotFound();
            }

            return View();
        }

        [HttpGet("/FacturaAdminController/ObtenerTodasLasFacturas/{pagina}")]
        public async Task<IActionResult> ObtenerTodosLasFacturas(int pagina)
        {
            //Si se intenta acceder por URL y por accidente se pone la pagina 0,
            //para que la aplicacion no se caiga
            if (pagina <= 0)
            {
                return BadRequest();
            }

            return new JsonResult(new { arregloFacturas = await facturaService.ObtenerFacturasPorPagina(pagina) });
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerTotalPaginas()
        {
            int totalPaginas = await facturaService.CalcularTotalPaginas();
            return new JsonResult(new { paginas = totalPaginas });
        }

        [HttpGet("/FacturaAdmin/ObtenerFacturaPorId/{idFactura}")]
        public async Task<IActionResult> ObtenerFacturaPorId(int idFactura)
        {
            return new JsonResult(new { factura = await facturaService.ObtenerPorIdFactura(idFactura) });
        }

        [HttpGet("/FacturaAdmin/ObtenerNotaCreditoPorIdFactura/{idFactura}")]
        public async Task<IActionResult> ObtenerNotaCreditoPorIdFactura(int idFactura)
        {
            return new JsonResult(new { notaCredito = await notaCreditoService.ObtenerNotaCreditoDTOPorIdFactura(idFactura) });
        }



        [HttpDelete("/FacturaAdmin/EliminarFactura/{idFactura}")]
        public async Task<IActionResult> EliminarFactura(int idFactura)
        {
            try
            {


                Factura facturaAEliminar = await facturaService.ObtenerPorIdFacturaNormal(idFactura);

                if (facturaAEliminar == null)
                {
                    return new JsonResult(new { mensaje = "Ha ocurrido un error al eliminar la factura", correcto = false });
                }

                // Si el estado del pedido es diferente a cancelado no se puede borrar el mismo
                if (facturaAEliminar.IdPedidoNavigation.IdEstadoPedido != 5)
                {
                    return new JsonResult(new { mensaje = "No puede eliminar la factura ya que esta no se encuentra cancelada", correcto = false });
                }

               

                await facturaService.Eliminar(facturaAEliminar);
                return new JsonResult(new { mensaje = "Factura Eliminada con exito", correcto = true });

            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al eliminar la factura", correcto = false });
            }
        }

        [HttpPost]

        public async Task<IActionResult> CrearNotaCredito([FromBody]int idFactura)
        {
            try
            {

                Notascredito notaCreditoBuscada = await notaCreditoService.ObtenerNotaCreditoPorIdFactura(idFactura);

                if (notaCreditoBuscada == null)
                {
                    Notascredito notaCreditoGuardar = new Notascredito
                    {
                        IdFactura = idFactura
                    };
                    await notaCreditoService.Guardar(notaCreditoGuardar);
                    return new JsonResult(new { mensaje = "Nota credito guardada con exito", correcto = true });
                }

                return new JsonResult(new { mensaje = "La nota de credito para esta factura ya ha sido creada con anterioridad", correcto = false });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al crear la nota de credito", correcto = false });
            }
        }


    }
}
