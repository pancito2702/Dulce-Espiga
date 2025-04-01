using System;
using System.Collections.Generic;

namespace BakeryApp_v1.Models;

public partial class Unidadesmedidum
{
    public int IdUnidad { get; set; }

    public string NombreUnidad { get; set; } = null!;

    public virtual ICollection<Ingrediente> Ingredientes { get; set; } = new List<Ingrediente>();
}
