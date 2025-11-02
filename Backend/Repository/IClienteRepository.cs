

using Backend.Models;

public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> GetAllClientes();
    Task<Cliente?> GetClienteById(int id);
    Task<Cliente?> GetClienteByName(string nome);
    Task AddCliente(Cliente cliente);
    Task UpdateCliente(Cliente cliente);
    Task DeleteCliente(int id);
}