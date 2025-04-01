using BakeryApp_v1.Models;
using BakeryApp_v1.Services;
using BakeryApp_v1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BakeryApp_v1.Controllers;

[Authorize(Policy = "SoloClientes")]
[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
public class CarritoController : Controller
{
    private readonly CarritoService carritoService;
    private readonly PersonaService personaService;
    private readonly ProductoService productoService;
    public CarritoController(CarritoService carritoService, PersonaService personaService, ProductoService productoService)
    {
        this.productoService = productoService;
        this.carritoService = carritoService;
        this.personaService = personaService;
    }



    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]

    public async Task<IActionResult> ObtenerCarritoUsuarioLogueado()
    {
        string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

        Persona personaABuscar = new Persona
        {
            Correo = correoUsuario
        };

        personaABuscar = await personaService.ObtenerPersonaPorCorreo(personaABuscar);


        return new JsonResult(new { arregloCarrito = await carritoService.ObtenerCarritoUsuario(personaABuscar.IdPersona) });
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AgregarCarrito([FromBody] Carritocompra carrito)
    {
        try
        {
            if (!carritoService.VerificarIdProducto(carrito))
            {
                return new JsonResult(new { mensaje = "Producto Invalido", correcto = false });
            }



            Producto productoBuscado = await productoService.ObtenerProductoPorId(carrito.IdProducto);

            if (productoBuscado == null)
            {
                return new JsonResult(new { mensaje = "Producto no encontrado", correcto = false });
            }
            // Se asigna el id del producto en el carrito al idProducto buscado
            carrito.IdProducto = productoBuscado.IdProducto;

            string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            Persona personaABuscar = new Persona
            {
                Correo = correoUsuario
            };

            personaABuscar = await personaService.ObtenerPersonaPorCorreo(personaABuscar);

            // Se asigna el id de la persona buscada al id de la persona en el carrito
            carrito.IdPersona = personaABuscar.IdPersona;


            Carritocompra carritoYaExiste = await carritoService.ObtenerCarritoPorIdProductoYIdUsuario(carrito.IdPersona, carrito.IdProducto);

            if (carritoYaExiste is not null)
            {
                // Se suma 1 a la cantidad en el carrito
                carritoYaExiste.CantidadProducto += 1;
                await carritoService.Editar(carritoYaExiste);
            }
            else
            {
                // Se suma 1 a la cantidad en el carrito
                carrito.CantidadProducto += 1;



                await carritoService.Guardar(carrito);
                return new JsonResult(new { mensaje = Url.Action("Index", "Carrito"), correcto = true });
            }


            return new JsonResult(new { mensaje = Url.Action("Index", "Carrito"), correcto = true });

        }
        catch (Exception ex)
        {
            return new JsonResult(new { mensaje = "Ha ocurrido un error al agregar al carrito de compras", correcto = false });
        }




    }




    [HttpDelete("/Carrito/EliminarCarrito/{idCarrito}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EliminarCarrito(int idCarrito)
    {
        try
        {
            if (!carritoService.VerificarIdValidoCarrito(idCarrito))
            {
                return new JsonResult(new { mensaje = "ID De carrito Invalido", correcto = false });
            }


            string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            Persona personaABuscar = new Persona
            {
                Correo = correoUsuario
            };

            personaABuscar = await personaService.ObtenerPersonaPorCorreo(personaABuscar);


            Carritocompra carritoAEliminar = await carritoService.ObtenerCarritoPorId(idCarrito);


            if (!carritoService.VerificarPerteneceCarritoUsuario(carritoAEliminar, personaABuscar.IdPersona))
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al eliminar del carrito", correcto = false });
            }




            await carritoService.Eliminar(carritoAEliminar);




            return new JsonResult(new { mensaje = "Producto eliminado con exito del carrito", correcto = true });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { mensaje = "Ha ocurrido un error al eliminar el producto del carrito de compras", correcto = false });
        }




    }


    [HttpPut]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> ModificarCarrito([FromBody] CarritoViewModel carrito)
    {
        try
        {
            if (!carritoService.VerificarIdValidoCarrito(carrito.IdCarrito))
            {
                return new JsonResult(new { mensaje = "ID De carrito Invalido", correcto = false });
            }


            string correoUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            Persona personaABuscar = new Persona
            {
                Correo = correoUsuario
            };

            personaABuscar = await personaService.ObtenerPersonaPorCorreo(personaABuscar);


            Carritocompra carritoAModificar = await carritoService.ObtenerCarritoPorId(carrito.IdCarrito);

            if (carritoAModificar == null)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al modificar el carrito", correcto = false });
            }




            if (!carritoService.VerificarPerteneceCarritoUsuario(carritoAModificar, personaABuscar.IdPersona))
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al modificar el carrito", correcto = false });
            }



            switch (carrito.Accion)
            {
                case "Agregar":
                    carritoAModificar.CantidadProducto += 1;
                    await carritoService.Editar(carritoAModificar);
                    break;
                case "Restar":
                    // Si la cantidad del carritoAModificar es 1, se retorna un mensaje de error
                    if (carritoAModificar.CantidadProducto == 1)
                    {
                        return new JsonResult(new { mensaje = "No se puede modificar el carrito, ya que la cantidad minima requerida del producto es 1", correcto = false });
                    }
                    carritoAModificar.CantidadProducto -= 1;
                    await carritoService.Editar(carritoAModificar);
                    break;
                default:
                    return new JsonResult(new { mensaje = "Ha ocurrido un error al modificar el carrito", correcto = false });
            }






            return new JsonResult(new { mensaje = "Producto modificado con exito del carrito", correcto = true });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { mensaje = "Ha ocurrido un error al eliminar el producto del carrito de compras", correcto = false });
        }




    }



}