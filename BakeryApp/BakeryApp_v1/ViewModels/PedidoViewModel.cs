using BakeryApp_v1.Models;

namespace BakeryApp_v1.ViewModels
{
    public class PedidoViewModel
    {
        public int IdPedido { get; set; }

        public int IdEstadoPedido { get; set; }

        public int IdPersona { get; set; }

        public int IdTipoEnvio { get; set; }

        public int IdTipoPago { get; set; }

        public int? IdDireccion { get; set; }

        public DateTime FechaPedido { get; set; }

        public PagoSinpeViewModel PagoSinpe { get; set; } 

        public static Pedido ConvertirPedidoViewModelAPedido(PedidoViewModel pedido)
        {
            return new Pedido
            {
                IdPedido = pedido.IdPedido,
                IdEstadoPedido = pedido.IdEstadoPedido,
                IdPersona = pedido.IdPersona,
                IdTipoEnvio = pedido.IdTipoEnvio,
                IdTipoPago = pedido.IdTipoPago,
                IdDireccion = pedido.IdDireccion,
                FechaPedido = pedido.FechaPedido,
            };
        }
        public static PedidoViewModel ConvertirPedidoAPedidoViewModel(Pedido pedido)
        {
            return new PedidoViewModel
            {
                IdPedido = pedido.IdPedido,
                IdEstadoPedido = pedido.IdEstadoPedido,
                IdPersona = pedido.IdPersona,
                IdTipoEnvio = pedido.IdTipoEnvio,
                IdTipoPago = pedido.IdTipoPago,
                IdDireccion = pedido.IdDireccion,
                FechaPedido = pedido.FechaPedido,
            };
        }

    }
}
