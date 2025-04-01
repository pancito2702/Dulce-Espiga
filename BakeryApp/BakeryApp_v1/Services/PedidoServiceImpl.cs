using BakeryApp_v1.DAO;
using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
using BakeryApp_v1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using System.Security.Claims;
using System.Security.Policy;
using Microsoft.Extensions.Configuration;
using PagedList;
using static System.Net.WebRequestMethods;
namespace BakeryApp_v1.Services
{
    public class PedidoServiceImpl : PedidoService
    {
        private readonly PedidoDAO pedidoDAO;

        private readonly IConfiguration configuracion;

        private readonly PersonaService personaService;


        public PedidoServiceImpl(PedidoDAO pedidoDAO, IConfiguration configuracion, PersonaService personaService)
        {
            this.pedidoDAO = pedidoDAO;

            this.configuracion = configuracion;
            this.personaService = personaService;
        }
        public async Task Editar(Pedido pedido)
        {
            await pedidoDAO.Editar(pedido);
        }

        public async Task Eliminar(Pedido pedido)
        {
            await pedidoDAO.Eliminar(pedido);
        }

        public async Task Guardar(Pedido pedido)
        {
            await pedidoDAO.Guardar(pedido);
        }

        public async Task<IEnumerable<PedidoDTO>> ObtenerPedidoPorCliente(int idCliente)
        {
            IEnumerable<PedidoDTO> todosLosPedidosCliente = await pedidoDAO.ObtenerPedidoPorCliente(idCliente);
            return todosLosPedidosCliente;
        }

        public async Task<Pedido> ObtenerPedidoPorId(int idPedido)
        {
            Pedido pedidoBuscado = await pedidoDAO.ObtenerPedidoPorId(idPedido);
            return pedidoBuscado;
        }

        public async Task<int> ObtenerCantidadPedidosNuevos()
        {
            int cantidadPedidos = await pedidoDAO.ObtenerCantidadPedidosNuevos();
            return cantidadPedidos;
        }

        public bool VerificarDatosVaciosOIncorrectos(Pedido pedido)
        {
            if (pedido.IdDireccion <= 0 || pedido.IdTipoEnvio <= 0 || pedido.IdTipoEnvio > 2 || pedido.IdTipoPago <= 0 || pedido.IdTipoPago > 3)
            {
                return false;
            }
            return true;
        }


        public decimal CalcularTotalPedido(IEnumerable<CarritoDTO> elementosCarrito)
        {
            decimal total = 0;

            foreach (CarritoDTO productoCarrito in elementosCarrito)
            {
                total += productoCarrito.CantidadProducto * productoCarrito.ProductoDTO.PrecioProducto;
            }

           
            return total;
        }


        public decimal CalcularIva(decimal total, PedidoViewModel pedido)
        {
            decimal iva = 0.14m;



            iva = total * iva;
            return iva;
        }

        public async Task<string> CrearSesionCheckoutStripe(PedidoViewModel pedido, IEnumerable<CarritoDTO> elementosCarrito, Pedido pedidoNormal)
        {

           
            StripeConfiguration.ApiKey = configuracion.GetValue<string>("StripeConfiguracion:SecretKey");

            decimal total = CalcularTotalPedido(elementosCarrito);

            // Si el envio es a domicilio
            if (pedido.IdTipoEnvio == 1)
            {
                total += 2500;
            }

            decimal iva = CalcularIva(total, pedido);
           
            

          

            // Se agregan los productos del carrito
            List<SessionLineItemOptions> itemsAPagar = elementosCarrito.Select(productoCarrito => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "CRC",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = productoCarrito.ProductoDTO.NombreProducto,
                    },
                    UnitAmount = (long)productoCarrito.ProductoDTO.PrecioProducto * 100
                },
                Quantity = productoCarrito.CantidadProducto
            }).ToList();


          


            // Si el envio es a domicilio se cargan los 2500 de envio
            if (pedido.IdTipoEnvio == 1)
            {

                itemsAPagar.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "CRC",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Costo de Envío",
                        },
                        UnitAmount = (2500 * 100),
                    },
                    Quantity = 1
                });

            }

            // Calcular y agregar IVA
            itemsAPagar.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "CRC",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = "IVA",
                    },
                    UnitAmount = (long)(iva * 100),
                },
                Quantity = 1
             
            });


            Persona personaPedido = await personaService.ObtenerPersonaPorId(pedidoNormal.IdPersona);

            SessionCreateOptions opcionesSesion = new SessionCreateOptions
            {
                CustomerEmail = personaPedido.Correo,
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = itemsAPagar,
                Mode = "payment",
                SuccessUrl = "https://localhost:7214/Pedido/Gracias",
   
                CancelUrl = "https://localhost:7214/Pedido/Checkout",
                PaymentIntentData = new SessionPaymentIntentDataOptions
                {
                    StatementDescriptor = "Dulce Espiga",
                    Description = "Compra en Dulce Espiga"
                },
                // Metadatos extras para guardarlos con el pedido cuando se complete el pago
                Metadata = new Dictionary<string, string>
                {
                    { "IdDireccion", pedido.IdDireccion.ToString()}, 
                    { "IdTipoEnvio", pedido.IdTipoEnvio.ToString()}
                },
            };
            

            SessionService servicioSesion = new SessionService();
            Session sesion = await servicioSesion.CreateAsync(opcionesSesion);


            return sesion.Id;

        }

      



        public string ObtenerTipoPagoPedido(PedidoViewModel pedido)
        {
            string tipoPago = "";
            switch (pedido.IdTipoPago)
            {
                case 1:
                    tipoPago = "Efectivo";
                    break;

                case 2:
                    tipoPago = "Sinpe Móvil";
                    break;

                case 3:
                    tipoPago = "Tarjeta";
                    break;
            }
            return tipoPago;
        }

        public string ObtenerTipoEnvioPedido(PedidoViewModel pedido)
        {
            string tipoEnvio = "";

            switch (pedido.IdTipoEnvio)
            {
                case 1:
                    tipoEnvio = "Entrega a domicilio";
                    break;
                case 2:
                    tipoEnvio = "Retirar en la sucursal";
                    break;
            }
            return tipoEnvio;
        }

        public async Task<IEnumerable<PedidoDTO>> ObtenerTodosLosPedidos(int pagina)
        {
            IEnumerable<PedidoDTO> todosLosPedidosPorPagina = await pedidoDAO.ObtenerTodosLosPedidos(pagina);
            return todosLosPedidosPorPagina;
        }

        public async Task<int> CalcularTotalPaginas()
        {
            int totalPedidos = await pedidoDAO.ContarTotalPedidos();
            int elementosPorPagina = 10;
            double totalPaginas = (double)totalPedidos / elementosPorPagina;
            totalPaginas = Math.Ceiling(totalPaginas);

            return (int)totalPaginas;
        }

        public async Task<PedidoDTO> ObtenerPedidoPorIdDTO(int idPedido)
        {
            PedidoDTO pedidoBuscado = await pedidoDAO.ObtenerPedidoPorIdDTO(idPedido); 
            return pedidoBuscado;
        }


        public bool VerificarPedidoPerteneceUsuario(Pedido pedido, int idUsuario)
        {
            if (pedido.IdPersona == idUsuario)
            {
                return true;
            }
            return false;

        }
    }
}
