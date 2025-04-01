using System;
using System.Collections.Generic;

namespace BakeryApp_v1.Models;

public partial class Receta
{
    public int IdReceta { get; set; }

    public string NombreReceta { get; set; } = null!;

    public string Instrucciones { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();

    public virtual ICollection<Ingrediente> IdIngredientes { get; set; } = new List<Ingrediente>();
}
