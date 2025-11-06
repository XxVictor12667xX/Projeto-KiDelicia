namespace Backend.Dtos
{
    public class PedidoCreateDto
    {
        public int ClienteId { get; set; }
        public List<ItemPedidoCreateDto> Itens { get; set; } = new();
    }
}
