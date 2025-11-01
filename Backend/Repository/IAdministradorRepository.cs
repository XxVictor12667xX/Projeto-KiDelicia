using Backend.Models;

public interface IAdministradorRepository
{
    Task<IEnumerable<Administrador>> GetAllAdministradores();
    Task<Administrador?> GetAdministradorById(int id);
    Task AddAdministrador(Administrador administrador);
    Task UpdateAdministrador(Administrador administrador);
    Task DeleteAdministrador(int id);
}