using BakeryApp_v1.Models;
using BakeryApp_v1.Services;
using BakeryApp_v1.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BakeryApp_v1.Controllers
{

    [Authorize(Policy = "SoloEmpleados")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class CategoriaEmpleadoController : Controller
    {
        private readonly CategoriaService categoriaService;
        private readonly IFuncionesUtiles funcionesUtiles;
        public CategoriaEmpleadoController(CategoriaService categoriaService, IFuncionesUtiles funcionesUtiles)
        {
            this.categoriaService = categoriaService;
            this.funcionesUtiles = funcionesUtiles;
        }

        public async Task<IActionResult> Index([FromQuery] int pagina)
        {
            int totalPaginas = await categoriaService.CalcularTotalPaginas();
            if (pagina > totalPaginas)
            {
                return NotFound();
            }

            return View();
        }

        public IActionResult AgregarCategoria()
        {
            return View();
        }

       
        public async Task<IActionResult> EditarCategoria([FromQuery] int idCategoria)
        {
            Categoria categoriaEditar = await categoriaService.ObtenerCategoriaPorId(idCategoria);

            if (categoriaEditar == null)
            {
                return NotFound();
            }


            return View();
        }

        [HttpGet("/CategoriaEmpleado/ObtenerCategorias/{pagina}")]
        public async Task<IActionResult> ObtenerCategorias(int pagina)
        {
            //Si se intenta acceder por URL y por accidente se pone la pagina 0,
            //para que la aplicacion no se caiga
            if (pagina <= 0)
            {
                return BadRequest();
            }

            return new JsonResult(new { arregloCategorias = await categoriaService.ObtenerTodasLasCategorias(pagina) });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> GuardarCategoria([FromForm] Categoria categoria)
        {
            try
            {

                if (categoriaService.VerificarDatosVaciosONulos(categoria))
                {
                    return new JsonResult(new { mensaje = "Hay datos vacios, por favor revise", correcto = false });
                }



                bool resultadoRepetida = await categoriaService.VerificarNombreRepetido(categoria);

                if (resultadoRepetida)
                {
                    return new JsonResult(new { mensaje = "El nombre de la categoria ya se encuentra registrado", correcto = false });
                }

                Categoria categoriaConImagen = await funcionesUtiles.GuardarImagenEnSistemaCategoria(categoria);

                if (categoriaConImagen == null)
                {
                    return new JsonResult(new { mensaje = "Error al guardar la imagen", correcto = false });
                }

                await categoriaService.Guardar(categoriaConImagen);
                return new JsonResult(new { mensaje = Url.Action("Index", "CategoriaEmpleado"), correcto = true, mensajeInfo = "Categoria guardada con exito" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al guardar la categoria" });
            }
        }


        [HttpDelete("/CategoriaEmpleado/EliminarCategoria/{idCategoria}")]
        [ValidateAntiForgeryToken]

        public async Task<JsonResult> EliminarCategoria(int idCategoria)
        {
            try
            {
                Categoria categoriaBorrarImagen = await categoriaService.ObtenerCategoriaPorId(idCategoria);

                if (!funcionesUtiles.BorrarImagenGuardadaEnSistemaCategoria(categoriaBorrarImagen))
                {
                    return new JsonResult(new { mensaje = "Ha ocurrido un error al borrar la imagen", correcto = false });
                }
                await categoriaService.Eliminar(categoriaBorrarImagen);
                return new JsonResult(new { mensaje = "Categoria eliminada con éxito", correcto = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al eliminar la categoria", correcto = false});
            }
        }


        [HttpGet("/CategoriaEmpleado/DevolverCategoriaEspecifica/{idCategoria}")]
        public async Task<JsonResult> DevolverCategoriaEspecifica(int idCategoria)
        {
            Categoria categoriaEncontrada = await categoriaService.ObtenerCategoriaPorId(idCategoria);
            return new JsonResult(new { categoria = categoriaEncontrada });
        }


        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> GuardarEditada([FromForm] Categoria categoria)
        {

            try
            {
                if (categoriaService.VerificarDatosVaciosONulos(categoria))
                {
                    return new JsonResult(new { mensaje = "Hay datos vacios, por favor revise", correcto = false });
                }


                bool resultadoRepetida = await categoriaService.VerificarNombreRepetido(categoria);

                if (resultadoRepetida)
                {
                    return new JsonResult(new { mensaje = "El nombre de la categoria ya se encuentra registrado", correcto = false });
                }

                Categoria categoriaConImagen = await funcionesUtiles.GuardarImagenEnSistemaCategoria(categoria);

                if (categoriaConImagen == null)
                {
                    return new JsonResult(new { mensaje = "Error al guardar la imagen", correcto = false });
                }

                await categoriaService.Editar(categoriaConImagen);
                return new JsonResult(new { mensaje = Url.Action("Index", "CategoriaEmpleado"), correcto = true, mensajeInfo = "Categoria modificado con exito" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = "Ha ocurrido un error al modificar la categoria", correcto = false});
            }
        }






        [HttpPost]
        public JsonResult BorrarImagenEditar([FromBody] Categoria categoria)
        {


            try
            {



                if (!funcionesUtiles.BorrarImagenGuardadaEnSistemaCategoria(categoria))
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
            int totalPaginas = await categoriaService.CalcularTotalPaginas();
            return new JsonResult(new { paginas = totalPaginas });
        }
    }
}
