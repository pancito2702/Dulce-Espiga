using BakeryApp_v1.Models;
using Microsoft.EntityFrameworkCore;
using PagedList;

namespace BakeryApp_v1.DAO;

public class UnidadMedidaDAOImpl : UnidadMedidaDAO
{

    private readonly BakeryAppContext dbContext;

    public UnidadMedidaDAOImpl(BakeryAppContext dbContext)
    {
        this.dbContext = dbContext;
    }


    public async Task<IEnumerable<Unidadesmedidum>> ObtenerTodasLasUnidadesDeMedida()
    {
        IEnumerable<Unidadesmedidum> todasLasUnidadesMedida = await dbContext.Unidadesmedida.ToListAsync();
        return todasLasUnidadesMedida;
    }


}
