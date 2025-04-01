using BakeryApp_v1.Models;
using System.Collections;

namespace BakeryApp_v1.DAO
{
    public interface BoletinNoticiasDAO 
    {
        public Task<IEnumerable<Boletinnoticia>> ObtenerTodosLosBoletines();

    }
}
