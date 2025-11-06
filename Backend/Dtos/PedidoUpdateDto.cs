using Backend.Models.Enum;

namespace Backend.Dtos
{
    public class PedidoUpdateDto
    {
        public int Id { get; set; }
        public PedidoStatus Status { get; set; } 
    }
}
