
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PedidoController : ControllerBase
{
    private readonly IPedidoRepository _pedidoRepository;

    public PedidoController(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPedidos()
    {

        var pedidos = await _pedidoRepository.GetAllPedidos();

        if (pedidos is null)
        {

            return NotFound("Nenhum pedido encontrado.");
        }
        else
        {

            return Ok(pedidos);
        }

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPedidoById(int id)
    {

        var pedido = await _pedidoRepository.GetPedidoById(id);

        if (pedido is null)
        {

            return NotFound("Pedido não encontrado.");
        }
        else
        {

            return Ok(pedido);
        }

    }

    [HttpPost]
    public async Task<IActionResult> CreatePedido([FromBody] Pedido pedido)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _pedidoRepository.AddPedido(pedido);
            return CreatedAtAction(nameof(GetPedidoById), new { id = pedido.Id }, pedido);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao criar o pedido: {ex.Message}");
        }

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePedido(int id, [FromBody] Pedido pedido)
    {
        if (id != pedido.Id)
        {
            return BadRequest("O ID informado não corresponde ao pedido enviado.");
        }

        var pedidoExistente = await _pedidoRepository.GetPedidoById(id);
        if (pedidoExistente is null)
        {
            return NotFound("Produto não encontrado para atualização.");
        }

        try
        {
            await _pedidoRepository.UpdatePedido(pedido);
            return Ok("Pedido atualizado com sucesso.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao atualizar o pedido: {ex.Message}");
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePedido(int id)
    {
        var pedidoExistente = await _pedidoRepository.GetPedidoById(id);
        if (pedidoExistente is null)
        {
            return NotFound("Pedido não encontrado para exclusão.");
        }

        try
        {
            await _pedidoRepository.DeletePedido(id);
            return Ok("Pedido excluído com sucesso.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao excluir o pedido: {ex.Message}");
        }
    }
}