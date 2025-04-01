using BakeryApp_v1.Models;

namespace BakeryApp_v1.DAO;

public interface RolDAO
{
    public Task<IEnumerable<Role>> ObtenerTodosLosRoles();
}


