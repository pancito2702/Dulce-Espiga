using System;
using System.Collections.Generic;

namespace BakeryApp_v1.Models;

public partial class Persona
{
    public int IdPersona { get; set; }

    public string Nombre { get; set; } = null!;

    public string PrimerApellido { get; set; } = null!;

    public string SegundoApellido { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contra { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public int IdRol { get; set; }

    public virtual ICollection<Boletin> Boletins { get; set; } = new List<Boletin>();

    public virtual ICollection<Carritocompra> Carritocompras { get; set; } = new List<Carritocompra>();

    public virtual ICollection<Direccionesusuario> Direccionesusuarios { get; set; } = new List<Direccionesusuario>();

    public virtual Role IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual ICollection<Recuperarcontra> Recuperarcontras { get; set; } = new List<Recuperarcontra>();
}
