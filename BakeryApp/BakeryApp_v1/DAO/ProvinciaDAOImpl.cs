using BakeryApp_v1.Models;
using Microsoft.EntityFrameworkCore;

namespace BakeryApp_v1.DAO;

public class ProvinciaDAOImpl : ProvinciaDAO
{
    private readonly BakeryAppContext dbContext;

    public ProvinciaDAOImpl(BakeryAppContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Provincia>> ObtenerTodasLasProvincias()
    {
        IEnumerable<Provincia> todasLasProvincias = await dbContext.Provincias.ToListAsync();

        return todasLasProvincias;
    }
}
