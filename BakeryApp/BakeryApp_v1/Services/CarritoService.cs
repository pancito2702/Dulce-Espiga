using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services;

public interface CarritoService
{
    public Task Guardar(Carritocompra carrito);

    public Task Editar(Carritocompra carrito);

    public Task Eliminar(Carritocompra carrito);

    public Task<IEnumerable<CarritoDTO>> ObtenerCarritoUsuario(int idUsuario);

    public Task<Carritocompra> ObtenerCarritoPorId(int idCarrito);

    public bool VerificarPerteneceCarritoUsuario(Carritocompra carrito, int idUsuario);

    public bool VerificarIdProducto(Carritocompra carrito);

    public Task<Carritocompra> ObtenerCarritoPorIdProductoYIdUsuario(int idUsuario, int idProducto);

    public bool VerificarIdValidoCarrito(int idCarrito);


}
