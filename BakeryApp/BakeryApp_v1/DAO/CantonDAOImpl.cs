using BakeryApp_v1.Models;
using Microsoft.EntityFrameworkCore;

namespace BakeryApp_v1.DAO;

public class CantonDAOImpl : CantonDAO
{
    private readonly BakeryAppContext dbContext;

    public CantonDAOImpl(BakeryAppContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Cantone>> ObtenerTodasLosCantonesPorProvincia(Provincia provincia)
    {
        IEnumerable<Cantone> cantonesPorProvincia = await dbContext.Cantones.Where(Canton => Canton.IdProvincia == provincia.IdProvincia).ToListAsync();
        return cantonesPorProvincia;
    }
}
