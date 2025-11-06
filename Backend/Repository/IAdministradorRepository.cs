using Backend.Dtos;
using Backend.Models;

public interface IAdministradorRepository
{
    Task<IEnumerable<Administrador>> GetAllAdministradores();
    Task<Administrador?> GetAdministradorById(int id);

    Task<Administrador?> GetByEmail(string email);

    Task AddAdministrador(Administrador administrador);
    Task UpdateAdministrador(Administrador administrador);
    Task DeleteAdministrador(int id);

    Task<bool> ValidarLogin(string email, string senha);
}