using Backend.Models;

public interface IAdministradorRepository
{
    Task<IEnumerable<Administrador>> GetAllAdministradores();
    Task<Administrador?> GetAdministradorById(int id);

    Task<Administrador?> GetByName(string nome);

    Task AddAdministrador(Administrador administrador);
    Task UpdateAdministrador(Administrador administrador);
    Task DeleteAdministrador(int id);

    Task<bool> ValidarLogin(string email, string senha);
}