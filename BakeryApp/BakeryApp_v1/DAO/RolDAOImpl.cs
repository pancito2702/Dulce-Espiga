using BakeryApp_v1.Models;
using Microsoft.EntityFrameworkCore;

namespace BakeryApp_v1.DAO;

public class RolDAOImpl : RolDAO
{
    private readonly BakeryAppContext dbContext;

    public RolDAOImpl(BakeryAppContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Role>> ObtenerTodosLosRoles()
    {
        List<Role> todosLosRoles = await dbContext.Roles.ToListAsync();
        return todosLosRoles;
    }
}
