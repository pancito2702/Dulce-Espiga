using System;
using System.Collections.Generic;

namespace BakeryApp_v1.Models;

public partial class Distrito
{
    public int IdDistrito { get; set; }

    public string NombreDistrito { get; set; } = null!;

    public int IdCanton { get; set; }

    public virtual ICollection<Direccionesusuario> Direccionesusuarios { get; set; } = new List<Direccionesusuario>();

    public virtual Cantone IdCantonNavigation { get; set; } = null!;
}
