
using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Administrador
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]

    public string Nome { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Senha { get; set; } = string.Empty;
}