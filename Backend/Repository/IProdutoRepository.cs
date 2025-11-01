using Backend.Models;

public interface IProdutoRepository
{
    Task<IEnumerable<Produto>> GetAllProdutos();
    Task<Produto> GetProdutoById(int id);
    Task AddProduto(Produto produto);
    Task UpdateProduto(Produto produto);
    Task DeleteProduto(int id);
}