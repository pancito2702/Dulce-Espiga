using BakeryApp_v1.Models;
using Microsoft.EntityFrameworkCore;

namespace BakeryApp_v1.DAO;

public class ReestablecerContraDAOImpl : ReestablecerContraDAO
{
    private readonly BakeryAppContext dbContext;

    public ReestablecerContraDAOImpl(BakeryAppContext dbContext)
    {
        this.dbContext = dbContext; 
    }

    public async Task Guardar(Recuperarcontra persona)
    {
        dbContext.Add(persona);
        await dbContext.SaveChangesAsync();
    }

    public async Task Eliminar(Recuperarcontra persona)
    {
        dbContext.Remove(persona);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Recuperarcontra> ObtenerPorIdPersona(Recuperarcontra persona)
    {
        Recuperarcontra personaContra = await dbContext.Recuperarcontras.FirstOrDefaultAsync(Persona => Persona.IdPersona == persona.IdPersona);
        return personaContra;
    }


    public async Task<int> ObtenerCantidadCodigosPersona(Recuperarcontra persona)
    {
       int cantidadCodigos = await dbContext.Recuperarcontras.Where(Persona => Persona.IdPersona == persona.IdPersona).CountAsync();
       return cantidadCodigos;
    }
}
