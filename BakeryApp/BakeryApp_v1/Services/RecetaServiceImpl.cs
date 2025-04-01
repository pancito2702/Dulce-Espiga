using BakeryApp_v1.DAO;
using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services;

public class RecetaServiceImpl : RecetaService
{
    private readonly RecetaDAO recetaDAO;

    public RecetaServiceImpl(RecetaDAO recetaDAO)
    {
        this.recetaDAO = recetaDAO;
    }

    public async Task Guardar(Receta receta)
    {
        await recetaDAO.Guardar(receta);
    }

    public async Task Editar(Receta receta)
    {
        await recetaDAO.Editar(receta);
    }

    public async Task Eliminar(Receta receta)
    {
        await recetaDAO.Eliminar(receta);
    }

    public async Task<IEnumerable<RecetaDTO>> ObtenerTodasLasRecetas(int pagina)
    {
        IEnumerable<RecetaDTO> todasLasRecetas = await recetaDAO.ObtenerTodasLasRecetas(pagina);
        return todasLasRecetas;
    }

    public async Task<Receta> ObtenerRecetaPorId(int idReceta)
    {
        Receta RecetaBuscada = await recetaDAO.ObtenerRecetaPorId(idReceta);
        return RecetaBuscada;
    }


    public async Task<RecetaDTO> ObtenerRecetaPorIdDTO(int idReceta)
    {
        RecetaDTO recetaBuscada = await recetaDAO.ObtenerRecetaPorIdDTO(idReceta);
        return recetaBuscada;
    }

    public async Task<IEnumerable<Receta>> ObtenerTodasLasRecetas()
    {
        IEnumerable<Receta> todasLasRecetas = await recetaDAO.ObtenerTodasLasRecetas();

        return todasLasRecetas;
    }

    public bool VerificarDatosVaciosONulos(Receta receta)
    {
        if (string.IsNullOrEmpty(receta.NombreReceta) || (string.IsNullOrEmpty(receta.Instrucciones)) || receta.IdIngredientes.ToList().Count == 0)

        {
            return true;
        }




        return false;
    }


    public async Task<bool> VerificarNombreRepetido(Receta receta)
    {
        Receta estaRepetida = await recetaDAO.ObtenerRecetaPorNombre(receta);

        if (estaRepetida == null)
        {
            return false;
        }

        return true;
    }

    public async Task<int> CalcularTotalPaginas()
    {
        int totalRecetas = await recetaDAO.ContarTotalRecetas();
        int elementosPorPagina = 9;
        double totalPaginas = (double)totalRecetas / elementosPorPagina;
        totalPaginas = Math.Ceiling(totalPaginas);

        return (int)totalPaginas;
    }



    public async Task LimpiarReceta(Receta receta)
    {
        await recetaDAO.LimpiarReceta(receta);
    }

}
