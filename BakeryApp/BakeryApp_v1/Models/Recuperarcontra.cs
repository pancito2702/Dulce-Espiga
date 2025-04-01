using System;
using System.Collections.Generic;

namespace BakeryApp_v1.Models;

public partial class Recuperarcontra
{
    public int IdRecuperacion { get; set; }

    public int IdPersona { get; set; }

    public string CodigoRecuperacion { get; set; } = null!;

    public DateTime FechaExpiracion { get; set; }

    public virtual Persona IdPersonaNavigation { get; set; } = null!;
}
