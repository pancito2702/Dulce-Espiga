using System;
using System.Collections.Generic;

namespace BakeryApp_v1.Models;

public partial class Cantone
{
    public int IdCanton { get; set; }

    public string NombreCanton { get; set; } = null!;

    public int IdProvincia { get; set; }

    public virtual ICollection<Direccionesusuario> Direccionesusuarios { get; set; } = new List<Direccionesusuario>();

    public virtual ICollection<Distrito> Distritos { get; set; } = new List<Distrito>();

    public virtual Provincia IdProvinciaNavigation { get; set; } = null!;
}
