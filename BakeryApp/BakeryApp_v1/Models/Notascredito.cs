using System;
using System.Collections.Generic;

namespace BakeryApp_v1.Models;

public partial class Notascredito
{
    public int IdNotaCredito { get; set; }

    public int IdFactura { get; set; }

    public virtual Factura IdFacturaNavigation { get; set; } = null!;
}
