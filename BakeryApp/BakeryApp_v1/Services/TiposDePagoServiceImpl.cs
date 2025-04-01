using BakeryApp_v1.DAO;
using BakeryApp_v1.Models;
using Humanizer;

namespace BakeryApp_v1.Services;

public class TiposDePagoServiceImpl : TiposDePagoService
{
    private readonly TiposPagoDAO tiposPagoDAO;

    public TiposDePagoServiceImpl(TiposPagoDAO tiposPagoDAO)
    {
        this.tiposPagoDAO = tiposPagoDAO;
    }

    public async Task<IEnumerable<Tipospago>> ObtenerTodosLosTiposDePago()
    {
        IEnumerable<Tipospago> todosLosTiposDePago = await tiposPagoDAO.ObtenerTodosLosTiposDePago();
        return todosLosTiposDePago;
    }


}

