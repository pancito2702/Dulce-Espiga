using System;
using System.Collections.Generic;

namespace BakeryApp_v1.Models;

public partial class Pedido
{
    public int IdPedido { get; set; }

    public int IdEstadoPedido { get; set; }

    public int IdPersona { get; set; }

    public int IdTipoEnvio { get; set; }

    public int IdTipoPago { get; set; }

    public int? IdDireccion { get; set; }

    public DateTime FechaPedido { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Direccionesusuario? IdDireccionNavigation { get; set; }

    public virtual Estadospedido IdEstadoPedidoNavigation { get; set; } = null!;

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual Tiposenvio IdTipoEnvioNavigation { get; set; } = null!;

    public virtual Tipospago IdTipoPagoNavigation { get; set; } = null!;

    public virtual ICollection<Pagossinpe> Pagossinpes { get; set; } = new List<Pagossinpe>();

    public virtual ICollection<Pedidoproducto> Pedidoproductos { get; set; } = new List<Pedidoproducto>();
}
