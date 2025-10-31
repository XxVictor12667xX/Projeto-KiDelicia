using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Itens
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int PedidoId { get; set; }

    [ForeignKey("PedidoId")]
    public Pedido Pedido { get; set; }

    [Required]
    public int ProdutoId { get; set; }

    [ForeignKey("ProdutoId")]
    public Produto Produto { get; set; }

    [Required]
    public int Quantidade { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal PrecoUnitario { get; set; }

    [NotMapped]
    public decimal Subtotal => Quantidade * PrecoUnitario;
}