using BakeryApp_v1.DAO;
using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
using System.Runtime.CompilerServices;

namespace BakeryApp_v1.Services;

public class ProductoServiceImpl : ProductoService
{
    private readonly ProductoDAO productoDAO;

    public ProductoServiceImpl(ProductoDAO productoDAO)
    {
        this.productoDAO = productoDAO;
    }

    public async Task Guardar(Producto producto)
    {
        await productoDAO.Guardar(producto);
    }

    public async Task Editar(Producto producto)
    {
        await productoDAO.Editar(producto);
    }

    public async Task Eliminar(Producto Producto)
    {
        await productoDAO.Eliminar(Producto);
    }

    public async Task<Producto> ObtenerProductoEspecifico(Producto producto)
    {
        Producto ProductoBuscado = await productoDAO.ObtenerProductoEspecifico(producto);
        return ProductoBuscado;
    }

    public async Task<Producto> ObtenerProductoPorId(int idProducto)
    {
        Producto ProductoBuscado = await productoDAO.ObtenerProductoPorId(idProducto);
        return ProductoBuscado;
    }

    public async Task<IEnumerable<ProductoDTO>> ObtenerTodasLasProductos(int pagina)
    {
        IEnumerable<ProductoDTO> todasLasProductos = await productoDAO.ObtenerTodasLasProductos(pagina);
        return todasLasProductos;
    }

    public bool VerificarDatosVaciosONulos(Producto producto)
    {
        if (string.IsNullOrEmpty(producto.NombreProducto) || (string.IsNullOrEmpty(producto.DescripcionProducto)) || producto.PrecioProducto == 0
            || producto.ArchivoProducto == null || producto.IdReceta == 0 || producto.IdCategoria == 0)
        {
            return true;
        }



        return false;
    }


    public bool VerificarPrecioPositivo(Producto producto)
    {

        if (producto.PrecioProducto <= 0)
        {
            return true;
        }
        return false;
    }


    public async Task<IEnumerable<ProductoDTO>> ObtenerTodasLasProductosPorCategoria(int idCategoria)
    {
        IEnumerable<ProductoDTO> productosPorCategoria = await productoDAO.ObtenerTodasLasProductosPorCategoria(idCategoria);
        return productosPorCategoria;
    }
    public async Task<bool> VerificarNombreRepetido(Producto producto)
    {
        Producto estaRepetida = await productoDAO.ObtenerProductoPorNombre(producto);

        if (estaRepetida == null)
        {
            return false;
        }

        return true;
    }

    public async Task<int> CalcularTotalPaginas()
    {
        int totalProductos = await productoDAO.ContarTotalProductos();
        int elementosPorPagina = 9;
        double totalPaginas = (double)totalProductos / elementosPorPagina;
        totalPaginas = Math.Ceiling(totalPaginas);

        return (int)totalPaginas;
    }





    public bool VerificarNombreVacio(string nombreProducto)
    {
        if (string.IsNullOrEmpty(nombreProducto))
        {
            return true;
        }
        return false;
    }


    public async Task<Producto> BuscarProductoPorNombre(string nombreProducto)
    {
        Producto productoBuscado = await productoDAO.ObtenerProductoPorNombre(nombreProducto);
        return productoBuscado;
    }

}
