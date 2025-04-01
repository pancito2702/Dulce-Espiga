using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;
using BakeryApp_v1.ViewModels;

namespace BakeryApp_v1.Utilidades;

public interface IMailEnviar
{
    public void Configurar();

    public Task<bool> EnviarCorreo(Persona persona, string asunto, string codigoRecuperacion);

    public Task<bool> EnviarCorreoPedidoConfirmado(Persona persona, string asunto, IEnumerable<CarritoDTO> todosLosElementosDelCarrito, PedidoViewModel pedido);

    public Task<bool> EnviarCorreoMarketingPersona(Boletin boletin, Mensajesboletin mensajeBoletin);


    public Task<bool> EnviarCorreoMarketingTodos(IEnumerable<Boletin> todosLosBoletinesUsuario, Mensajesboletin mensajeBoletin);
}
