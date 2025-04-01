using BakeryApp_v1.DTO;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.DAO;

public interface PersonaDAO
{
    public Task Guardar(Persona persona);

    public Task Editar(Persona persona);


    public Task Eliminar(Persona persona);

    public Task<Persona> ObtenerPersonaEspecifica(Persona persona);

    public Task<Persona> ObtenerPersonaPorId(int idPersona);

    public Task<PersonaDTO> ObtenerPersonaConRoles(Persona persona);

    public Task<IEnumerable<PersonaDTO>> ObtenerTodasLasPersonas(int pagina);

    public Task<Persona> ObtenerPersonaPorCorreo(Persona persona);

    public Task<Persona> ObtenerPersonaPorTelefono(Persona persona);

    public Task<int> ContarTotalPersonas();
}
