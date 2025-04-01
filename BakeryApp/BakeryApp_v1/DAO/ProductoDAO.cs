using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.DAO;

public interface ProductoDAO
{
    public Task Guardar(Producto producto);

    public Task Editar(Producto producto);


    public Task Eliminar(Producto producto);

    public Task<Producto> ObtenerProductoEspecifico(Producto producto);

    public Task<Producto> ObtenerProductoPorId(int idProducto);

    public Task<IEnumerable<ProductoDTO>> ObtenerTodasLasProductos(int pagina);

    public Task<IEnumerable<ProductoDTO>> ObtenerTodasLasProductosPorCategoria(int idCategoria);

    public Task<Producto> ObtenerProductoPorNombre(Producto producto);

    public Task<Producto> ObtenerProductoPorNombre(string nombreProducto);
    public Task<int> ContarTotalProductos();

}
