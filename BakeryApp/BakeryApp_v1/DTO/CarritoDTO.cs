using BakeryApp_v1.Models;

namespace BakeryApp_v1.DTO;

public class CarritoDTO
{
    public int IdCarrito { get; set; }

    public int IdPersona { get; set; }

    public int IdProducto { get; set; }

    public int CantidadProducto { get; set; }

    public virtual ProductoDTO? ProductoDTO { get; set; }

    public static Pedidoproducto ConvertirCarritoAProductoPedido(CarritoDTO elementoCarrito, int idPedido)
    {
        return new Pedidoproducto
        {
            IdPedido = idPedido,
            CantidadProducto = elementoCarrito.CantidadProducto,
            IdProducto = elementoCarrito.IdProducto
        };
    }

    public static Carritocompra ConvertirCarritoDTOACarrito(CarritoDTO elementoCarrito)
    {
        return new Carritocompra
        {
            IdCarrito = elementoCarrito.IdCarrito,
            IdPersona = elementoCarrito.IdPersona,
            IdProducto = elementoCarrito.IdProducto,
            CantidadProducto = elementoCarrito.CantidadProducto
        };
    }

}
