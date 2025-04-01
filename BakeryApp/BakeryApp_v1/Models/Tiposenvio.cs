using System;
using System.Collections.Generic;

namespace BakeryApp_v1.Models;

public partial class Tiposenvio
{
    public int IdTipoEnvio { get; set; }

    public string NombreTipo { get; set; } = null!;

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
