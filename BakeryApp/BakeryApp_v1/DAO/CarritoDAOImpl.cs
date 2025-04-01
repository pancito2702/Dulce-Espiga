using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
using Microsoft.EntityFrameworkCore;

namespace BakeryApp_v1.DAO;
public class CarritoDAOImpl : CarritoDAO
{
    private readonly BakeryAppContext context;

    public CarritoDAOImpl(BakeryAppContext context)
    {
        this.context = context;
    }

    public async Task Editar(Carritocompra carrito)
    {
        context.Update(carrito);
        await context.SaveChangesAsync();
    }

    public async Task Eliminar(Carritocompra carrito)
    {
        context.Remove(carrito);
        await context.SaveChangesAsync();
    }

    public async Task Guardar(Carritocompra carrito)
    {
        context.Add(carrito);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CarritoDTO>> ObtenerCarritoUsuario(int idUsuario)
    {
        IEnumerable<CarritoDTO> carritoUsuario = await context.Carritocompras.Include(Carrito => Carrito.IdProductoNavigation).Where(Carrito => Carrito.IdPersona == idUsuario).Select(Carrito => new CarritoDTO
        {
            IdCarrito = Carrito.IdCarrito,
            IdPersona = Carrito.IdPersona,
            IdProducto = Carrito.IdProducto,
            CantidadProducto = Carrito.CantidadProducto,
            ProductoDTO = new ProductoDTO
            {
                NombreProducto = Carrito.IdProductoNavigation.NombreProducto,
                ImagenProducto = Carrito.IdProductoNavigation.ImagenProducto,
                PrecioProducto = Carrito.IdProductoNavigation.PrecioProducto,
            }
        }).ToListAsync();
        return carritoUsuario;
    }

    public async Task<Carritocompra> ObtenerCarritoPorIdProductoYIdUsuario(int idUsuario, int idProducto)
    {
        Carritocompra carritoYaExiste = await context.Carritocompras.FirstOrDefaultAsync(Carrito => Carrito.IdPersona == idUsuario && Carrito.IdProducto == idProducto);
        return carritoYaExiste;
    }

    public async Task<Carritocompra> ObtenerCarritoPorId(int idCarrito)
    {
        Carritocompra carritoBuscado = await context.Carritocompras.FirstOrDefaultAsync(Carrito => Carrito.IdCarrito == idCarrito);


        return carritoBuscado;
    }
}

