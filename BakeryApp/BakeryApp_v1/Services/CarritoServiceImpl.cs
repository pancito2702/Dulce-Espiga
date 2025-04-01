using BakeryApp_v1.DAO;
using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services;

public class CarritoServiceImpl : CarritoService
{
    private readonly CarritoDAO carritoDAO;

    public CarritoServiceImpl(CarritoDAO carritoDAO)
    {
        this.carritoDAO = carritoDAO;
    }   

    public async Task Editar(Carritocompra carrito)
    {
        await carritoDAO.Editar(carrito);
    }

    public async Task Eliminar(Carritocompra carrito)
    {
        await carritoDAO.Eliminar(carrito);
    }

    public async Task Guardar(Carritocompra carrito)
    {
        await carritoDAO.Guardar(carrito);
    }

    public async Task<IEnumerable<CarritoDTO>> ObtenerCarritoUsuario(int idUsuario)
    {
        IEnumerable<CarritoDTO> carritoUsuario = await carritoDAO.ObtenerCarritoUsuario(idUsuario);
        return carritoUsuario;
    }

    public bool VerificarIdProducto(Carritocompra carrito)
    {

        if (carrito.IdProducto <= 0)
        {
            return false;
        }
        return true;

    }

    public async Task<Carritocompra> ObtenerCarritoPorIdProductoYIdUsuario(int idUsuario, int idProducto)
    {
        Carritocompra carritoYaExiste = await carritoDAO.ObtenerCarritoPorIdProductoYIdUsuario(idUsuario, idProducto);
        return carritoYaExiste;
    }

    public async Task<Carritocompra> ObtenerCarritoPorId(int idCarrito)
    {
        Carritocompra carritoBuscado = await carritoDAO.ObtenerCarritoPorId(idCarrito);
        return carritoBuscado;
    }

    public bool VerificarPerteneceCarritoUsuario(Carritocompra carrito, int idUsuario)
    {
        if (carrito.IdPersona != idUsuario)
        {
            return false;
        }
        return true;
    }

    public bool VerificarIdValidoCarrito(int idCarrito)
    {
        if (idCarrito <= 0)
        {
            return false;
        }
        return true;

    }

  

}
