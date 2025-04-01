using BakeryApp_v1.DAO;
using BakeryApp_v1.Models;
using BakeryApp_v1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BakeryApp_v1.Controllers
{

    [Authorize(Policy = "SoloAdministradores")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]

    public class PedidoAdminController : Controller
    {
        private readonly PedidoService pedidoService;
        private readonly EstadosPedidoService estadosPedidoService;
        public PedidoAdminController(PedidoService pedidoService, EstadosPedidoService estadosPedidoService)
        {
            this.pedidoService = pedidoService;
            this.estadosPedidoService = estadosPedidoService;
        }


        public async Task<IActionResult> Pedidos([FromQuery] int pagina)
        {
            int totalPaginas = await pedidoService.CalcularTotalPaginas();
            if (pagina > totalPaginas)
            {
                return NotFound();
            }

            return View();

        }


        public async Task<IActionResult> VerPedido([FromQuery] int idPedido)
        {
            Pedido pedidoABuscar = await pedidoService.ObtenerPedidoPorId(idPedido);
            if (pedidoABuscar == null)
            {
                return NotFound();
            }
            return View();
        }


        [HttpGet("/PedidoAdmin/ObtenerTodosLosPedidos/{pagina}")]
        public async Task<IActionResult> ObtenerTodosLosPedidos(int pagina)
        {
            //Si se intenta acceder por URL y por accidente se pone la pagina 0,
            //para que la aplicacion no se caiga
            if (pagina <= 0)
            {
                return BadRequest();
            }

            return new JsonResult(new { arregloPedidos = await pedidoService.ObtenerTodosLosPedidos(pagina) });
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerTotalPaginas()
        {
            int totalPaginas = await pedidoService.CalcularTotalPaginas();
            return new JsonResult(new { paginas = totalPaginas });
        }

    
        
        public async Task<IActionResult> ObtenerEstadosPedido()
        {
            return new JsonResult(new { arregloEstadosPedido = await estadosPedidoService.ObtenerTodosLosEstadosPedido() });
        }


        [HttpPut]
        [ValidateAntiForgeryToken]        
        

        public async Task<IActionResult> ActualizarEstadoPedido([FromBody] Pedido pedido)
        {
            try
            {
                if (pedido == null)
                {
                    return new JsonResult(new { mensaje = "Ha ocurrido un error al modificar el estado del pedido", correcto = false });
                }

                Pedido pedidoAModificar = await pedidoService.ObtenerPedidoPorId(pedido.IdPedido);

                if (pedidoAModificar == null)
                {
                    return new JsonResult(new { mensaje = "Ha ocurrido un error al modificar el estado del pedido", correcto = false });
                }

                // Se asigna el id del estado del pedido buscado al id del estado del pedido recibido
                pedidoAModificar.IdEstadoPedido = pedido.IdEstadoPedido;


                await pedidoService.Editar(pedidoAModificar);

                return new JsonResult(new { mensaje = "Estado modificado con exito", correcto = true });

            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al modificar el estado del pedido", correcto = false });
            }
        }


       
        [HttpGet("/PedidoAdmin/ObtenerPedidoDTOPorId/{idPedido}")]

        public async Task<IActionResult> ObtenerPedidoPorId(int idPedido)
        {
            return new JsonResult(new { pedido = await pedidoService.ObtenerPedidoPorIdDTO(idPedido) });
        }


    }
}
