using Backend.Models;
using KiDelicia.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace KiDelicia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministradorController : ControllerBase
    {
        private readonly IAdministradorRepository _adminRepository;
        private readonly TokenService _tokenService;


        public AdministradorController(IAdministradorRepository adminRepository, TokenService tokenService)
        {
            _adminRepository = adminRepository;
            _tokenService = tokenService;
        }

 
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Administrador adminLogin)
        {
            if (string.IsNullOrWhiteSpace(adminLogin.Email) || string.IsNullOrWhiteSpace(adminLogin.Senha))
                return BadRequest("Email e senha são obrigatórios.");

            var valido = await _adminRepository.ValidarLogin(adminLogin.Email, adminLogin.Senha);
            if (!valido)
                return Unauthorized("Credenciais inválidas.");

            var admin = await _adminRepository.GetByName(adminLogin.Email);
            var token = _tokenService.GenerateToken(admin!);

            return Ok(new
            {
                message = "Login realizado com sucesso.",
                token
            });
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllAdministradores()
        {
            var admins = await _adminRepository.GetAllAdministradores();

            if (admins is null || !admins.Any())
                return NotFound("Nenhum administrador encontrado.");

            return Ok(admins);
        }


        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdministradorById(int id)
        {
            var admin = await _adminRepository.GetAdministradorById(id);
            if (admin is null)
                return NotFound("Administrador não encontrado.");

            return Ok(admin);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAdministrador([FromBody] Administrador admin)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _adminRepository.AddAdministrador(admin);
                return CreatedAtAction(nameof(GetAdministradorById), new { id = admin.Id }, admin);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar administrador: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdministrador(int id, [FromBody] Administrador admin)
        {
            if (id != admin.Id)
                return BadRequest("O ID informado não corresponde ao administrador enviado.");

            var adminExistente = await _adminRepository.GetAdministradorById(id);
            if (adminExistente is null)
                return NotFound("Administrador não encontrado para atualização.");

            try
            {
                await _adminRepository.UpdateAdministrador(admin);
                return Ok("Administrador atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar administrador: {ex.Message}");
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdministrador(int id)
        {
            var adminExistente = await _adminRepository.GetAdministradorById(id);
            if (adminExistente is null)
                return NotFound("Administrador não encontrado para exclusão.");

            try
            {
                await _adminRepository.DeleteAdministrador(id);
                return Ok("Administrador excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir administrador: {ex.Message}");
            }
        }
    }
}
