using BakeryApp_v1.Models;
using Microsoft.EntityFrameworkCore;
namespace BakeryApp_v1.DAO;

public class DistritoDAOImpl : DistritoDAO
{
    private readonly BakeryAppContext dbContext;

    public DistritoDAOImpl(BakeryAppContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Distrito>> ObtenerTodasLosDistritosPorCanton(Cantone canton)
    {
        IEnumerable<Distrito> distritosPorCanton = await dbContext.Distritos.Where(Canton => Canton.IdCanton == canton.IdCanton).ToListAsync();
        return distritosPorCanton;
    }
}
