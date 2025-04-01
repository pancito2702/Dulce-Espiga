using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services;

public interface PersonaService
{
    public Task Guardar(Persona persona);

    public Task Editar(Persona persona);


    public Task Eliminar(Persona persona);

    public Task<Persona> ObtenerPersonaEspecifica(Persona persona);

    public Task<Persona> ObtenerPersonaPorId(int idPersona);

    public Task<Persona> ObtenerPersonaPorCorreo(Persona persona);
    public bool VerificarDatosVaciosONulos(Persona persona);

    public bool VerificarNombreApellidosOTelefonoNulos(Persona persona);
    public bool ValidarLongitudContra(Persona persona);

    public Task<IEnumerable<PersonaDTO>> ObtenerTodasLasPersonas(int pagina);


    public Task<bool> VerificarCorreoRepetido(Persona persona);

    public Task<bool> VerificarTelefonoRepetido(Persona persona);

    public Task<int> CalcularTotalPaginas();

    public bool ValidarLongitudTelefono(Persona persona);
    public bool ValidarNumeroTelefono(Persona persona);

    public bool VerificarCorreoElectronico(Persona persona);

    public bool VerificarRolPersona(Persona persona);


    public bool VerificarCorreoOContraVacia(Persona persona);

    public bool VerificarContraConContraUsuario(Persona persona, Persona personaEncontrada);

    public bool VerificarCorreoVacio(Persona persona);

    public bool VerificarContraVacia(Persona persona);

    public Task<PersonaDTO> ObtenerPersonaConRoles(Persona persona);

    public Task<int> ContarTotalPersonas();
}
