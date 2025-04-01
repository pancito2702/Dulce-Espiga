using System;
using System.Collections.Generic;

namespace BakeryApp_v1.Models;

public partial class Boletinnoticia
{
    public int IdBoletinNoticias { get; set; }

    public string NombreBoletin { get; set; } = null!;

    public virtual ICollection<Boletin> Boletins { get; set; } = new List<Boletin>();
}
