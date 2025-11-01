using Backend.Models;
using Microsoft.EntityFrameworkCore;

public class AdministradorRepository: IAdministradorRepository
{
    private readonly AplicacaoContext _context;

    public AdministradorRepository(AplicacaoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Administrador>> GetAllAdministradores()
    {
        return await _context.Administradores.ToListAsync();
    }

    public async Task<Administrador?> GetAdministradorById(int id)
    {
        return await _context.Administradores.FindAsync(id);
    }

    public async Task AddAdministrador(Administrador administrador)
    {
        await _context.Administradores.AddAsync(administrador);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAdministrador(Administrador administrador)
    {
        _context.Administradores.Update(administrador);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAdministrador(int id)
    {
        var administrador = await _context.Administradores.FindAsync(id);
        if (administrador != null)
        {
            _context.Administradores.Remove(administrador);
            await _context.SaveChangesAsync();
        }
    }

}