using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
using Microsoft.EntityFrameworkCore;
namespace BakeryApp_v1.DAO
{
    public class ProductoPedidoDAOImpl : ProductoPedidoDAO
    {
        private readonly BakeryAppContext dbContext;

        public ProductoPedidoDAOImpl(BakeryAppContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task Guardar(Pedidoproducto productoPedido)
        {
            dbContext.Add(productoPedido);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Pedidoproducto>> ObtenerTodosLosProductosPorPedido(int idPedido)
        {
            IEnumerable<Pedidoproducto> todosLosProductosPorPedido = await dbContext.Pedidoproductos.Include(Producto => Producto.IdProductoNavigation).Where(ProductoPedido => ProductoPedido.IdPedido == idPedido).ToListAsync();
            return todosLosProductosPorPedido;
        }
        public async Task<IEnumerable<ProductoPedidoDTO>> ObtenerProductosMasVendidos()
        {
            IEnumerable<ProductoPedidoDTO> top3Productos = await dbContext.Pedidoproductos
            .Include(Producto => Producto.IdProductoNavigation)
            .GroupBy(Producto => Producto.IdProductoNavigation.IdProducto)
            .Select(productoVentas => new ProductoPedidoDTO
            {
                IdProducto = productoVentas.Key, 
                CantidadProducto = productoVentas.Sum(p => p.CantidadProducto),
                Producto = new ProductoDTO
                {
                    NombreProducto = productoVentas.Select(p => p.IdProductoNavigation.NombreProducto).FirstOrDefault() ?? "Sin nombre",  
                    ImagenProducto = productoVentas.Select(p => p.IdProductoNavigation.ImagenProducto).FirstOrDefault() ?? "Sin imagen",
                    PrecioProducto = productoVentas.Select(p => p.IdProductoNavigation.PrecioProducto).FirstOrDefault()
                }
            })
            .OrderByDescending(p => p.CantidadProducto)  
            .Take(3)  
            .ToListAsync();
            return top3Productos;
        }


    }
}

