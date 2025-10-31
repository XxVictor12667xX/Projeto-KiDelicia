
using System.ComponentModel.DataAnnotations;
using Backend.Models.Enum;


namespace Backend.Models;

public class Produto
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do produto é obrigatório")]
    public required string Nome { get; set; }

    [Required(ErrorMessage = "O produto precisa ter uma descrição")]
    public required string Descricao { get; set; }

    [Required(ErrorMessage = "O produto precisa ter uma categoria")]
    public Categoria CategoriaProduto { get; set; }

    [Required(ErrorMessage = "O produto precisa ter um preço")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "O produto precisa ter uma imagem")]
    public required string UrlImagem { get; set; }

}