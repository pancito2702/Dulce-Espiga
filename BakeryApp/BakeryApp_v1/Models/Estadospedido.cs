using System;
using System.Collections.Generic;

namespace BakeryApp_v1.Models;

public partial class Estadospedido
{
    public int IdEstadoPedido { get; set; }

    public string NombreEstado { get; set; } = null!;

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
