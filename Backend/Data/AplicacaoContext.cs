
using Backend.Models;
using Microsoft.EntityFrameworkCore;

public class AplicacaoContext: DbContext
{
    public AplicacaoContext( DbContextOptions<AplicacaoContext> options) : base(options)
    {

    }
    
    public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Itens> PedidoItens { get; set; }
        public DbSet<Administrador> Administradores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Definir comportamento de deleção em cascata
            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.Itens)
                .WithOne(i => i.Pedido)
                .OnDelete(DeleteBehavior.Cascade);
        }
    
}