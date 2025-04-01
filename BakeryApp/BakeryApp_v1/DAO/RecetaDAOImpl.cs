using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
using Microsoft.EntityFrameworkCore;
using PagedList;

namespace BakeryApp_v1.DAO;

public class RecetaDAOImpl : RecetaDAO
{
    private readonly BakeryAppContext dbContext;

    public RecetaDAOImpl(BakeryAppContext dbContext)
    {
        this.dbContext = dbContext;
    }


    public async Task Guardar(Receta receta)
    {
        dbContext.Add(receta);
        await dbContext.SaveChangesAsync();
    }

    public async Task Editar(Receta receta)
    {
        dbContext.Update(receta);
        await dbContext.SaveChangesAsync();
    }


    public async Task Eliminar(Receta receta)
    {
        dbContext.Remove(receta);
        await dbContext.SaveChangesAsync();
    }


    public async Task<RecetaDTO> ObtenerRecetaPorIdDTO(int idReceta)
    {
        Receta recetaEncontrada = await dbContext.Recetas.Include(Receta => Receta.IdIngredientes).FirstOrDefaultAsync(Receta => Receta.IdReceta == idReceta);

        return RecetaDTO.ConvertirRecetaARecetaDTO(recetaEncontrada);
    }

    public async Task<Receta> ObtenerRecetaPorId(int idReceta)
    {

        Receta recetaEncontrada = await dbContext.Recetas.Include(Receta => Receta.IdIngredientes).FirstOrDefaultAsync(Receta => Receta.IdReceta == idReceta);

        return recetaEncontrada;
    }

    public async Task<IEnumerable<Receta>> ObtenerTodasLasRecetas()
    {
        IEnumerable<Receta> todasLasRecetas = await dbContext.Recetas.ToListAsync();

        return todasLasRecetas;
    }


    public async Task<IEnumerable<RecetaDTO>> ObtenerTodasLasRecetas(int pagina)
    {
        int numeroDeElementosPorPagina = 9;


        IPagedList<RecetaDTO> todasLasRecetas = dbContext.Recetas
       .Include(receta => receta.IdIngredientes)
       .OrderBy(receta => receta.IdReceta)
       .Select(receta => new RecetaDTO
       {
           IdReceta = receta.IdReceta,
           NombreReceta = receta.NombreReceta,
           Instrucciones = receta.Instrucciones,
           Ingredientes = receta.IdIngredientes.Select(ingrediente => new IngredienteDTO
           {
               IdIngrediente = ingrediente.IdIngrediente,
               NombreIngrediente = ingrediente.NombreIngrediente
           }).ToList()

       }).ToPagedList(pageNumber: pagina, pageSize: numeroDeElementosPorPagina);

        return todasLasRecetas;
    }

    public async Task<Receta> ObtenerRecetaPorNombre(Receta receta)
    {
        Receta RecetaEncontrada = await dbContext.Recetas.FirstOrDefaultAsync(Receta => Receta.NombreReceta == receta.NombreReceta && Receta.IdReceta != receta.IdReceta);
        return RecetaEncontrada;
    }



    public async Task<int> ContarTotalRecetas()
    {
        int totalRecetas = await dbContext.Recetas.CountAsync();
        return totalRecetas;
    }



    public async Task LimpiarReceta(Receta receta)
    {
        // Se busca la receta actual y se incluyen los ingredientes
        Receta recetaExistenteConIngredientes = await dbContext.Recetas
               .Include(r => r.IdIngredientes)
               .FirstOrDefaultAsync(r => r.IdReceta == receta.IdReceta);

        // Se limpia el arreglo de los ingredientes
        recetaExistenteConIngredientes.IdIngredientes.Clear();

        // Se guardan los cambios
        await dbContext.SaveChangesAsync();

        // Se pone la entidad como detached del contexto, para que se le deje de dar siguimiento 
        dbContext.Entry(recetaExistenteConIngredientes).State = EntityState.Detached;
    }
}
 
