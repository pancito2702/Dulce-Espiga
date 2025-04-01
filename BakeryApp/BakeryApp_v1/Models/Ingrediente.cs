using System;
using System.Collections.Generic;

namespace BakeryApp_v1.Models;

public partial class Ingrediente
{
    public int IdIngrediente { get; set; }

    public string NombreIngrediente { get; set; } = null!;

    public string DescripcionIngrediente { get; set; } = null!;

    public int CantidadIngrediente { get; set; }

    public int UnidadMedidaIngrediente { get; set; }

    public decimal PrecioUnidadIngrediente { get; set; }

    public DateTime FechaCaducidadIngrediente { get; set; }

    public virtual Unidadesmedidum UnidadMedidaIngredienteNavigation { get; set; } = null!;

    public virtual ICollection<Receta> IdReceta { get; set; } = new List<Receta>();
}
