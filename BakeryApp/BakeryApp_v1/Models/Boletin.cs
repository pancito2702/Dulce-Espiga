using System;
using System.Collections.Generic;

namespace BakeryApp_v1.Models;

public partial class Boletin
{
    public int IdBoletin { get; set; }

    public int IdBoletinNoticias { get; set; }

    public int IdUsuario { get; set; }

    public virtual Boletinnoticia IdBoletinNoticiasNavigation { get; set; } = null!;

    public virtual Persona IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Mensajesboletin> Mensajesboletins { get; set; } = new List<Mensajesboletin>();
}
