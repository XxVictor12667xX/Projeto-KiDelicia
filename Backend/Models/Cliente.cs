using System.ComponentModel.DataAnnotations;


namespace Backend.Models;

public class Cliente
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do cliente é obrigatório")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O número da mesa é obrigatório")]
    public int Mesa { get; set; }
}