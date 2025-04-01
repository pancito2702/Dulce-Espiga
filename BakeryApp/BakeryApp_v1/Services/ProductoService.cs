using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services;

public interface ProductoService
{
    public Task Guardar(Producto producto);

    public Task Editar(Producto producto);
    public Task Eliminar(Producto producto);

    public Task<Producto> ObtenerProductoEspecifico(Producto producto);

    public Task<Producto> ObtenerProductoPorId(int idProducto);

    public Task<IEnumerable<ProductoDTO>> ObtenerTodasLasProductos(int pagina);

    public bool VerificarDatosVaciosONulos(Producto producto);

    public bool VerificarPrecioPositivo(Producto producto);

    public Task<bool> VerificarNombreRepetido(Producto producto);

    public Task<int> CalcularTotalPaginas();

    public Task<IEnumerable<ProductoDTO>> ObtenerTodasLasProductosPorCategoria(int idCategoria);


    public bool VerificarNombreVacio(string nombreProducto);
  
    public Task<Producto> BuscarProductoPorNombre(string nombreProducto);   

}
