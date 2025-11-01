
using Backend.Models;
using Microsoft.EntityFrameworkCore;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AplicacaoContext _context;


    public ProdutoRepository(AplicacaoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Produto>> GetAllProdutos()
    {
        return await _context.Produtos.ToListAsync();
    }

    public async Task<Produto?> GetProdutoById(int id)
    {
        return  await _context.Produtos.FindAsync(id);
    }

    public async Task  AddProduto(Produto produto)
    {
        await _context.Produtos.AddAsync(produto);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProduto(Produto produto)
    {
        _context.Produtos.Update(produto);
        await _context.SaveChangesAsync();
    }
    

    public async Task DeleteProduto(int id)
    {
        var produto = _context.Produtos.Find(id);
        if (produto != null)
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }
    }
    
}