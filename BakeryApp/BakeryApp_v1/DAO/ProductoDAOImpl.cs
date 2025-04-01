
using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
using Microsoft.EntityFrameworkCore;
using PagedList;

namespace BakeryApp_v1.DAO;

public class ProductoDAOImpl : ProductoDAO
{
    private readonly BakeryAppContext dbContext;

    public ProductoDAOImpl(BakeryAppContext dbContext)
    {
        this.dbContext = dbContext;
    }


    public async Task Guardar(Producto producto)
    {
        dbContext.Add(producto);
        await dbContext.SaveChangesAsync();
    }

    public async Task Editar(Producto producto)
    {
        dbContext.Update(producto);
        await dbContext.SaveChangesAsync();
    }


    public async Task Eliminar(Producto producto)
    {
        dbContext.Remove(producto);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Producto> ObtenerProductoEspecifico(Producto producto)
    {
        
        Producto productoEncontrado = await dbContext.Productos.FirstOrDefaultAsync(Producto => Producto.IdProducto == producto.IdProducto);
        return productoEncontrado;
    }

    public async Task<Producto> ObtenerProductoPorId(int idProducto)
    {
        Producto productoEncontrado = await dbContext.Productos.FirstOrDefaultAsync(Producto => Producto.IdProducto == idProducto);
        return productoEncontrado;
    }


    public async Task<Producto> ObtenerProductoPorNombre(string nombreProducto)
    {
        Producto productoBuscado = await dbContext.Productos.FirstOrDefaultAsync(Producto => Producto.NombreProducto == nombreProducto);

        return productoBuscado;
    }

    public async Task<IEnumerable<ProductoDTO>> ObtenerTodasLasProductos(int pagina)
    {
        int numeroDeElementosPorPagina = 9;

        IPagedList<ProductoDTO> todasLasProductos = dbContext.Productos.Include(Producto => Producto.IdRecetaNavigation).Include(Producto => Producto.IdCategoriaNavigation).OrderBy(Producto => Producto.IdProducto).Select
            (Producto => new ProductoDTO
            {
                IdProducto = Producto.IdProducto,
                NombreProducto = Producto.NombreProducto,
                DescripcionProducto = Producto.DescripcionProducto,
                PrecioProducto = Producto.PrecioProducto,
                ImagenProducto = Producto.ImagenProducto,
                Receta = new RecetaDTO
                {
                    NombreReceta = Producto.IdRecetaNavigation.NombreReceta  
                },
                Categoria = new CategoriaDTO
                {
                    NombreCategoria = Producto.IdCategoriaNavigation.NombreCategoria
                }

            }).ToPagedList(pageNumber: pagina, pageSize: numeroDeElementosPorPagina);
        return todasLasProductos;
    }

    public async Task<Producto> ObtenerProductoPorNombre(Producto producto)
    {
        Producto productoEncontrado = await dbContext.Productos.FirstOrDefaultAsync(Producto => Producto.NombreProducto == producto.NombreProducto && Producto.IdProducto != producto.IdProducto);
        return productoEncontrado;
    }

    public async Task<int> ContarTotalProductos()
    {
        int totalProductos = await dbContext.Productos.CountAsync();
        return totalProductos;
    }

    public async Task<IEnumerable<ProductoDTO>> ObtenerTodasLasProductosPorCategoria(int idCategoria)
    {
        IEnumerable<ProductoDTO> todosLosProductosPorCategoria = await dbContext.Productos.Include(Producto => Producto.IdCategoriaNavigation).Where(Producto => Producto.IdCategoria == idCategoria)
            .Select(Producto => new ProductoDTO
            {
                IdProducto = Producto.IdProducto,
                NombreProducto = Producto.NombreProducto,
                DescripcionProducto = Producto.DescripcionProducto,
                PrecioProducto = Producto.PrecioProducto,
                ImagenProducto = Producto.ImagenProducto,
                IdCategoria = Producto.IdCategoria
            }).ToListAsync();

        return todosLosProductosPorCategoria;
    }

}
