using System.Security.Cryptography;
using System.Text;
using Backend.Dtos;
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

    public async Task<Administrador?> GetByEmail(string email)
        {
            return await _context.Administradores
                .FirstOrDefaultAsync(a => a.Email.ToLower() == email.ToLower());
        }

    public async Task AddAdministrador(Administrador administrador)
    {   
        administrador.Senha = HashSenha(administrador.Senha );
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
    

    public async Task<bool> ValidarLogin(string email, string senha)
        {
            var admin = await GetByEmail(email);
            if (admin == null) return false;

            var hash = HashSenha(senha);
            return admin.Senha == hash;
    }

    private string HashSenha(string senha)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
        return Convert.ToBase64String(bytes);
    }

    
}