using BakeryApp_v1.Models;

namespace BakeryApp_v1.Services;

public interface RolService
{
    public Task<IEnumerable<Role>> ObtenerTodosLosRoles();
}
