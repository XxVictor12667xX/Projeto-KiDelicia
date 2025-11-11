using Backend.Dtos;
using Backend.Models;

public interface IPedidoRepository
{
    Task<IEnumerable<Pedido>> GetAllPedidos();
    Task<Pedido> GetPedidoById(int id);
    Task AddPedido(Pedido pedido);
    Task UpdatePedido(Pedido pedido);
    Task DeletePedido(int id);
}