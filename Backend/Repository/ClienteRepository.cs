

using Backend.Models;
using Microsoft.EntityFrameworkCore;

public class ClienteRepository: IClienteRepository
{
    private readonly AplicacaoContext _context;

    public ClienteRepository(AplicacaoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cliente>> GetAllClientes()
    {
        return await _context.Clientes.ToListAsync();
    }


    public async Task<Cliente?> GetClienteById(int id)
    {
        return await _context.Clientes.FindAsync(id);
    }

    public async Task<Cliente?> GetClienteByName(string nome)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(c => c.Nome.ToLower() == nome.ToLower());
        }

    public async Task AddCliente(Cliente cliente)
    {
        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCliente(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCliente(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente != null)
        {
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }
    }


}