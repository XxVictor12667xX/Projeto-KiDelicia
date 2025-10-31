using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Models.Enum;


namespace Backend.Models;
public class Pedido
{

    [Key]
    public int Id { get; set; }

    [Required]
    public int ClienteId { get; set; }

    [ForeignKey("ClienteId")]
    public Cliente Cliente { get; set; }

    public DateTime DataHora { get; set; } = DateTime.Now;

    public PedidoStatus Status { get; set; } = PedidoStatus.Recebido;

    [Column(TypeName = "decimal(10,2)")]
    public decimal ValorTotal { get; set; }

    public ICollection<Itens> Itens { get; set; } = new List<Itens>();

}