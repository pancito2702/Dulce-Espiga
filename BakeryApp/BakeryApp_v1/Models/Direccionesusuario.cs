using System;
using System.Collections.Generic;

namespace BakeryApp_v1.Models;

public partial class Direccionesusuario
{
    public int IdDireccion { get; set; }

    public string NombreDireccion { get; set; } = null!;

    public int IdPersona { get; set; }

    public int IdProvincia { get; set; }

    public int IdCanton { get; set; }

    public int IdDistrito { get; set; }

    public string DireccionExacta { get; set; } = null!;

    public virtual Cantone IdCantonNavigation { get; set; } = null!;

    public virtual Distrito IdDistritoNavigation { get; set; } = null!;

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual Provincia IdProvinciaNavigation { get; set; } = null!;

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
