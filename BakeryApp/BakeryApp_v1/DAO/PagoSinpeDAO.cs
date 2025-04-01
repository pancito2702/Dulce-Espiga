using BakeryApp_v1.Models;

namespace BakeryApp_v1.DAO;

public interface PagoSinpeDAO
{
    public Task GuardarPagoSinpe(Pagossinpe pagoSinpe);

    public Task EditarPagoSinpe(Pagossinpe pagoSinpe);

    public Task<IEnumerable<Pagossinpe>> ObtenerTodosLosPagosSinpe();

    public Task<Pagossinpe> ObtenerPagoSinpePorPedido(int idPedido);



}
