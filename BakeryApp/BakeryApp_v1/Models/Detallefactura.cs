using System;
using System.Collections.Generic;

namespace BakeryApp_v1.Models;

public partial class Detallefactura
{
    public int Linea { get; set; }

    public int IdFactura { get; set; }

    public int IdPedidoProducto { get; set; }

    public decimal TotalLinea { get; set; }

    public virtual Factura IdFacturaNavigation { get; set; } = null!;

    public virtual Pedidoproducto IdPedidoProductoNavigation { get; set; } = null!;
}
