using BakeryApp_v1.DAO;
using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services;

public class IngredienteServiceImpl : IngredienteService
{
    private readonly IngredienteDAO ingredienteDAO;

    public IngredienteServiceImpl(IngredienteDAO ingredienteDAO)
    {
        this.ingredienteDAO = ingredienteDAO;
    }

    public async Task Guardar(Ingrediente ingrediente)
    {
        await ingredienteDAO.Guardar(ingrediente);
    }

    public async Task Editar(Ingrediente ingrediente)
    {
        await ingredienteDAO.Editar(ingrediente);
    }

    public async Task Eliminar(Ingrediente ingrediente)
    {
        await ingredienteDAO.Eliminar(ingrediente);
    }



    public async Task<Ingrediente> ObtenerIngredientePorId(int idIngrediente)
    {
        Ingrediente ingredienteBuscada = await ingredienteDAO.ObtenerIngredientePorId(idIngrediente);
        return ingredienteBuscada;
    }

    public async Task<IngredienteDTO> ObtenerIngredienteDTOPorId(int idIngrediente)
    {
        IngredienteDTO ingredienteBuscada = await ingredienteDAO.ObtenerIngredienteDTOPorId(idIngrediente);
        return ingredienteBuscada;
    }

    public async Task<IEnumerable<IngredienteDTO>> ObtenerTodasLasIngredientes(int pagina)
    {
        IEnumerable<IngredienteDTO> todasLasIngredientes = await ingredienteDAO.ObtenerTodasLasIngredientes(pagina);
        return todasLasIngredientes;
    }

    public async Task<IEnumerable<Ingrediente>> ObtenerTodasLasIngredientes()
    {
        IEnumerable<Ingrediente> todosLosIngredientes = await ingredienteDAO.ObtenerTodasLasIngredientes();
        return todosLosIngredientes;
    }

    public bool VerificarDatosVaciosONulos(Ingrediente ingrediente)
    {
        if (string.IsNullOrEmpty(ingrediente.NombreIngrediente) || (string.IsNullOrEmpty(ingrediente.DescripcionIngrediente))
            || ingrediente.UnidadMedidaIngrediente == 0)

        {
            return true;
        }




        return false;
    }

    public bool VerificarFechaVencimiento(Ingrediente ingrediente)
    {
        if (ingrediente.FechaCaducidadIngrediente < DateTime.Today)
        {
            return true;
        }

        return false;
    }


    public bool VerificarPrecioPositivo(Ingrediente ingrediente)
    {

        if (ingrediente.PrecioUnidadIngrediente <= 0)
        {
            return true;
        }
        return false;
    }

    public bool VerificarCantidadPositiva(Ingrediente ingrediente)
    {

        if (ingrediente.CantidadIngrediente <= 0)
        {
            return true;
        }
        return false;
    }

    public async Task<bool> VerificarNombreRepetido(Ingrediente ingrediente)
    {
        Ingrediente estaRepetida = await ingredienteDAO.ObtenerIngredientePorNombre(ingrediente);

        if (estaRepetida == null)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> VerificarIngredienteConReceta(int idIngrediente)
    {
        Ingrediente ingredienteConReceta = await ingredienteDAO.ObtenerIngredientePorId(idIngrediente);

        Console.WriteLine(ingredienteConReceta.IdReceta.Count());

        if (ingredienteConReceta.IdReceta.Count() == 0)
        {
            return false;
        }
      

        return true;
    }

    public async Task<int> CalcularTotalPaginas()
    {
        int totalIngredientes = await ingredienteDAO.ContarTotalIngredientes();
        int elementosPorPagina = 10;
        double totalPaginas = (double)totalIngredientes / elementosPorPagina;
        totalPaginas = Math.Ceiling(totalPaginas);

        return (int)totalPaginas;
    }

    public void AttachIngredientes(IEnumerable<Ingrediente> ingredientes)
    {
        ingredienteDAO.IngredientesAttach(ingredientes);
    }
}
