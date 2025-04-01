using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
using BakeryApp_v1.Services;
using BakeryApp_v1.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BakeryApp_v1.Controllers;
[Authorize(Policy = "SoloAdministradores")]
[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
public class RecetaController : Controller
{
    private readonly IngredienteService ingredienteService;
    private readonly RecetaService recetaService;
    public RecetaController(IngredienteService ingredienteService, RecetaService recetaService)
    {
        this.ingredienteService = ingredienteService;
        this.recetaService = recetaService;
    }



    public async Task<IActionResult> Index([FromQuery] int pagina)
    {
        int totalPaginas = await recetaService.CalcularTotalPaginas();
        if (pagina > totalPaginas)
        {
            return NotFound();
        }


        return View();
    }


    public IActionResult AgregarReceta()
    {
        return View();
    }

    public async Task<IActionResult> EditarReceta([FromQuery] int idReceta)
    {
        Receta recetaEditar = await recetaService.ObtenerRecetaPorId(idReceta);

        if (recetaEditar == null)
        {
            return NotFound();
        }


        return View();
    }



    [HttpGet]
    public async Task<JsonResult> ObtenerTotalPaginas()
    {
        int totalPaginas = await recetaService.CalcularTotalPaginas();
        return new JsonResult(new { paginas = totalPaginas });
    }

    [HttpGet("/Receta/ObtenerTodosLosIngredientes")]

    public async Task<JsonResult> ObtenerTodosLosIngredientes()
    {
        return new JsonResult(new { arregloIngredientes = await ingredienteService.ObtenerTodasLasIngredientes() });
    }

    [HttpGet("/Receta/DevolverRecetaEspecifica/{idReceta}")]
    public async Task<JsonResult> DevolverRecetaEspecifica(int idReceta)
    {
        RecetaDTO recetaEncontrada = await recetaService.ObtenerRecetaPorIdDTO(idReceta);
        return new JsonResult(new { receta = recetaEncontrada });
    }

    [HttpGet("/Recetas/ObtenerTodasLasRecetas/{pagina}")]
    public async Task<IActionResult> ObtenerTodasLasRecetas(int pagina)
    {
        if (pagina <= 0)
        {
            return BadRequest();
        }
        return new JsonResult(new { arregloRecetas = await recetaService.ObtenerTodasLasRecetas(pagina) });
    }


    [HttpPut]
    [ValidateAntiForgeryToken]

    public async Task<JsonResult> GuardarEditada([FromBody] Receta receta)
    {

        try
        {

            if (recetaService.VerificarDatosVaciosONulos(receta))
            {
                return new JsonResult(new { mensaje = "Hay datos vacios, por favor revise", correcto = false});
            }



            bool resultadoRepetida = await recetaService.VerificarNombreRepetido(receta);

            if (resultadoRepetida)
            {
                return new JsonResult(new { mensaje = "El nombre de la receta ya se encuentra registrado", correcto = false});
            }



            await recetaService.LimpiarReceta(receta);


            // Se buscan los ingredientes relacionados a la receta
            List<Ingrediente> ingredientesBuscados = new List<Ingrediente>();
            foreach (Ingrediente ingrediente in receta.IdIngredientes)
            {
                Ingrediente ingredienteEncontrado = await ingredienteService.ObtenerIngredientePorId(ingrediente.IdIngrediente);
                ingredientesBuscados.Add(ingredienteEncontrado);
            }


            //Se crea la receta con los ingredientes asociados
            Receta recetaConIngredientes = new Receta
            {
                IdReceta = receta.IdReceta,
                NombreReceta = receta.NombreReceta,
                Instrucciones = receta.Instrucciones,
                IdIngredientes = ingredientesBuscados
            };




            ingredienteService.AttachIngredientes(ingredientesBuscados);

            await recetaService.Editar(recetaConIngredientes);


            return new JsonResult(new { mensaje = Url.Action("Index", "Receta"), correcto = true, mensajeInfo = "Receta modificada con exito" });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new JsonResult(new { mensaje = "Ha ocurrido un error al modificar la receta", correcto = false });
        }

    }





    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<JsonResult> GuardarReceta([FromBody] Receta receta)
    {

        try
        {

            if (recetaService.VerificarDatosVaciosONulos(receta))
            {
                return new JsonResult(new { mensaje = "Hay datos vacios, por favor revise", correcto = false });
            }



            bool resultadoRepetida = await recetaService.VerificarNombreRepetido(receta);

            if (resultadoRepetida)
            {
                return new JsonResult(new { mensaje = "El nombre de la receta ya se encuentra registrado", correcto = false });
            }



            // Se buscan los ingredientes relacionados a la receta
            List<Ingrediente> ingredientesBuscados = new List<Ingrediente>();
            foreach (Ingrediente ingrediente in receta.IdIngredientes)
            {
                Ingrediente ingredienteEncontrado = await ingredienteService.ObtenerIngredientePorId(ingrediente.IdIngrediente);
                ingredientesBuscados.Add(ingredienteEncontrado);
            }


            //Se crea la receta con los ingredientes asociados
            Receta recetaConIngredientes = new Receta
            {
                IdReceta = receta.IdReceta,
                NombreReceta = receta.NombreReceta,
                Instrucciones = receta.Instrucciones,
                IdIngredientes = ingredientesBuscados
            };




            ingredienteService.AttachIngredientes(ingredientesBuscados);

            await recetaService.Guardar(recetaConIngredientes);



            return new JsonResult(new { mensaje = Url.Action("Index", "Receta"), correcto = true, mensajeInfo = "Receta agregada con exito" });

        }
        catch (Exception ex)
        {

            return new JsonResult(new { mensaje = "Ha ocurrido un error al guardar la receta", correcto = false });
        }

    }


    [HttpDelete("/Receta/EliminarReceta/{idReceta}")]
    [ValidateAntiForgeryToken]

    public async Task<JsonResult> EliminarReceta(int idReceta)
    {
        try
        {
            Receta recetaAEliminar = await recetaService.ObtenerRecetaPorId(idReceta);

            await recetaService.Eliminar(recetaAEliminar);
            return new JsonResult(new { mensaje = "Receta eliminada con éxito", correcto = true });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { mensaje = "Ha ocurrido un error al eliminar la receta", correcto = false});
        }
    }


}
