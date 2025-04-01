using System;
using System.Collections.Generic;

namespace BakeryApp_v1.Models;

public partial class Mensajesboletin
{
    public int IdMensajeBoletin { get; set; }

    public int IdBoletin { get; set; }

    public string Mensaje { get; set; } = null!;

    public string Asunto { get; set; } = null!;

    public virtual Boletin IdBoletinNavigation { get; set; } = null!;
}
