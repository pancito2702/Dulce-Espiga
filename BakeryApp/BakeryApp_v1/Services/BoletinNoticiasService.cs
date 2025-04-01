using BakeryApp_v1.Models;
using System.Collections;

namespace BakeryApp_v1.Services
{
    public interface BoletinNoticiasService
    {
        public Task<IEnumerable<Boletinnoticia>> ObtenerTodosLosBoletines();

    }
}
