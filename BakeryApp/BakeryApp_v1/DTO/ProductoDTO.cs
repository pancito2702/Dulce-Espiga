using BakeryApp_v1.Models;

namespace BakeryApp_v1.DTO;

public class ProductoDTO
{
    public int IdProducto { get; set; }

    public string NombreProducto { get; set; } = null!;

    public string DescripcionProducto { get; set; } = null!;

    public decimal PrecioProducto { get; set; }

    public int IdCategoria { get; set; }

    public int IdReceta { get; set; }

    public string ImagenProducto { get; set; } = null!;

    public virtual CategoriaDTO Categoria { get; set; } = null!;

    public virtual RecetaDTO Receta { get; set; } = null!;

}
