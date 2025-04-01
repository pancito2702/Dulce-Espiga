using System;
using System.Collections.Generic;

namespace BakeryApp_v1.Models;

public partial class Pedidoproducto
{
    public int IdPedidoProducto { get; set; }

    public int IdProducto { get; set; }

    public int IdPedido { get; set; }

    public int CantidadProducto { get; set; }

    public virtual ICollection<Detallefactura> Detallefacturas { get; set; } = new List<Detallefactura>();

    public virtual Pedido IdPedidoNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
