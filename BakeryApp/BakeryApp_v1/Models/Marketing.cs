using System;
using System.Collections.Generic;

namespace BakeryApp_v1.Models;

public partial class Marketing
{
    public int IdMarketing { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;
}
