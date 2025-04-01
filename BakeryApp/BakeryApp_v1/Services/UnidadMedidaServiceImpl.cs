using BakeryApp_v1.DAO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services;

public class UnidadMedidaServiceImpl : UnidadMedidaService
{
    private readonly UnidadMedidaDAO unidadMedidaDAO;

    public UnidadMedidaServiceImpl(UnidadMedidaDAO unidadMedidaDAO)
    {
        this.unidadMedidaDAO = unidadMedidaDAO;
    }


    public async Task<IEnumerable<Unidadesmedidum>> ObtenerTodasLasUnidadesDeMedida()
    {
        IEnumerable<Unidadesmedidum> todasLasUnidadesDeMedida = await unidadMedidaDAO.ObtenerTodasLasUnidadesDeMedida();
        return todasLasUnidadesDeMedida;
    }

}
