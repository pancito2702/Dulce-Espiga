using BakeryApp_v1.Models;
using BakeryApp_v1.Services;
using BakeryApp_v1.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BakeryApp_v1.Controllers
{
    [Authorize(Policy = "SoloAdministradores")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class ProductoController : Controller
    {
        private readonly ProductoService productoService;
        private readonly CategoriaService categoriaService;
        private readonly RecetaService recetaService;   
        private readonly IFuncionesUtiles funcionesUtiles;
        public ProductoController(ProductoService productoService, IFuncionesUtiles funcionesUtiles, CategoriaService categoriaService, RecetaService recetaService)
        {
            this.productoService = productoService;
            this.funcionesUtiles = funcionesUtiles;
            this.categoriaService = categoriaService;
            this.recetaService = recetaService;
        }

        public async Task<IActionResult> Index([FromQuery] int pagina)
        {
            int totalPaginas = await productoService.CalcularTotalPaginas();
            if (pagina > totalPaginas)
            {
                return NotFound();
            }

            return View();
        }

        public IActionResult AgregarProducto()
        {
            return View();
        }

       
        public async Task<IActionResult> EditarProducto([FromQuery] int idProducto)
        {
            Producto productoEditar = await productoService.ObtenerProductoPorId(idProducto);

            if (productoEditar == null)
            {
                return NotFound();
            }


            return View();
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerTodosLasRecetas()
        {
            IEnumerable<Receta> todasLasRecetas = await recetaService.ObtenerTodasLasRecetas();


            return new JsonResult(new { arregloRecetas = todasLasRecetas });
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerTodosLasCategorias()
        {
            IEnumerable<Categoria> todasLasCategorias = await categoriaService.ObtenerTodasLasCategorias();


            return new JsonResult(new { arregloCategorias = todasLasCategorias });
        }


        [HttpGet("/Producto/ObtenerProductos/{pagina}")]
        public async Task<IActionResult> ObtenerProductos(int pagina)
        {
            //Si se intenta acceder por URL y por accidente se pone la pagina 0,
            //para que la aplicacion no se caiga
            if (pagina <= 0)
            {
                return BadRequest();
            }

            return new JsonResult(new { arregloProductos = await productoService.ObtenerTodasLasProductos(pagina) });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> GuardarProducto([FromForm] Producto producto)
        {
            try
            {

                if (productoService.VerificarDatosVaciosONulos(producto))
                {
                    return new JsonResult(new { mensaje = "Hay datos vacios, por favor revise", correcto = false });
                }



                bool resultadoRepetida = await productoService.VerificarNombreRepetido(producto);

                if (resultadoRepetida)
                {
                    return new JsonResult(new { mensaje = "El nombre del producto ya se encuentra registrado", correcto = false });
                }


                if (productoService.VerificarPrecioPositivo(producto))
                {
                    return new JsonResult(new { mensaje = "El precio del producto no puede ser 0 o negativo", correcto = false });
                }

              

                bool resultadoGuardarImagen = await funcionesUtiles.GuardarImagenEnSistemaProducto(producto);

                if (!resultadoGuardarImagen)
                {
                    return new JsonResult(new { mensaje = "Error al guardar la imagen", correcto = false });
                }

               


              

                await productoService.Guardar(producto);


                return new JsonResult(new { mensaje = Url.Action("Index", "Producto"), correcto = true, mensajeInfo = "Producto guardado con exito" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al guardar el producto", correcto = false });
            }
        }


        [HttpDelete("/Producto/EliminarProducto/{idProducto}")]
        [ValidateAntiForgeryToken]

        public async Task<JsonResult> EliminarProducto(int idProducto)
        {
            try
            {
                Producto productoBorrarImagen = await productoService.ObtenerProductoPorId(idProducto);

                if (!funcionesUtiles.BorrarImagenGuardadaEnSistemaProducto(productoBorrarImagen))
                {
                    return new JsonResult(new { mensaje = "Ha ocurrido un error al borrar la imagen", correcto = false });
                }


              
                await productoService.Eliminar(productoBorrarImagen);
                return new JsonResult(new { mensaje = "Producto eliminado con éxito", correcto = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al eliminar el producto", correcto = false});
            }
        }


        [HttpGet("/Producto/DevolverProductoEspecifico/{idProducto}")]
        public async Task<JsonResult> DevolverProductoEspecifico(int idProducto)
        {
            Producto productoEncontrada = await productoService.ObtenerProductoPorId(idProducto);
            return new JsonResult(new { Producto = productoEncontrada });
        }


        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> GuardarEditado([FromForm] Producto producto)
        {

            try
            {
                if (productoService.VerificarDatosVaciosONulos(producto))
                {
                    return new JsonResult(new { mensaje = "Hay datos vacios, por favor revise", correcto = false });
                }


                bool resultadoRepetida = await productoService.VerificarNombreRepetido(producto);

                if (resultadoRepetida)
                {
                    return new JsonResult(new { mensaje = "El nombre del producto ya se encuentra registrado", correcto = false });
                }


                if (productoService.VerificarPrecioPositivo(producto))
                {
                    return new JsonResult(new { mensaje = "El precio del producto no puede ser 0 o negativo", correcto = false });
                }





                bool resultadoGuardarImagen = await funcionesUtiles.GuardarImagenEnSistemaProducto(producto);

                if (!resultadoGuardarImagen)
                {
                    return new JsonResult(new { mensaje = "Error al guardar la imagen", correcto = false });
                }

              

                await productoService.Editar(producto);


                return new JsonResult(new { mensaje = Url.Action("Index", "Producto"), correcto = true, mensajeInfo = "Producto modificado con exito" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al modificar el producto", correcto = false});
            }
        }






        [HttpPost]
        public JsonResult BorrarImagenEditar([FromBody] Producto Producto)
        {


            try
            {



                if (!funcionesUtiles.BorrarImagenGuardadaEnSistemaProducto(Producto))
                {
                    return new JsonResult(new { mensaje = "Ha ocurrido un error al borrar la imagen", correcto = false });
                }



                return new JsonResult(new { correcto = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error", correcto = false });
            }




        }

        [HttpGet]
        public async Task<JsonResult> ObtenerTotalPaginas()
        {
            int totalPaginas = await productoService.CalcularTotalPaginas();
            return new JsonResult(new { paginas = totalPaginas });
        }
    }
}
