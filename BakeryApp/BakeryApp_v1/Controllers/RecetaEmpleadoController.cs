using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
using BakeryApp_v1.Services;
using BakeryApp_v1.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BakeryApp_v1.Controllers;
[Authorize(Policy = "SoloEmpleados")]
[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
public class RecetaEmpleadoController : Controller
{
    private readonly IngredienteService ingredienteService;
    private readonly RecetaService recetaService;
    public RecetaEmpleadoController(IngredienteService ingredienteService, RecetaService recetaService)
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

 
    [HttpGet]
    public async Task<JsonResult> ObtenerTotalPaginas()
    {
        int totalPaginas = await recetaService.CalcularTotalPaginas();
        return new JsonResult(new { paginas = totalPaginas });
    }

    

  

    [HttpGet("/RecetasEmpleado/ObtenerTodasLasRecetas/{pagina}")]
    public async Task<IActionResult> ObtenerTodasLasRecetas(int pagina)
    {
        if (pagina <= 0)
        {
            return BadRequest();
        }
        return new JsonResult(new { arregloRecetas = await recetaService.ObtenerTodasLasRecetas(pagina) });
    }


   


}
