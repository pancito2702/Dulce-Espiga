using System;
using System.Collections.Generic;

namespace BakeryApp_v1.Models;

public partial class Provincia
{
    public int IdProvincia { get; set; }

    public string NombreProvincia { get; set; } = null!;

    public virtual ICollection<Cantone> Cantones { get; set; } = new List<Cantone>();

    public virtual ICollection<Direccionesusuario> Direccionesusuarios { get; set; } = new List<Direccionesusuario>();
}
