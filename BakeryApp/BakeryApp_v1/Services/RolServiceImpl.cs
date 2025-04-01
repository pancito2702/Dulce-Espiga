using BakeryApp_v1.DAO;
using BakeryApp_v1.Models;


namespace BakeryApp_v1.Services;

public class RolServiceImpl : RolService
{
    private readonly RolDAO rolDAO;

    public RolServiceImpl(RolDAO rolDAO)
    {
        this.rolDAO = rolDAO;
    }

    public async Task<IEnumerable<Role>> ObtenerTodosLosRoles()
    {
        IEnumerable<Role> todosLosRoles = await rolDAO.ObtenerTodosLosRoles();
        return todosLosRoles;
    }
}
