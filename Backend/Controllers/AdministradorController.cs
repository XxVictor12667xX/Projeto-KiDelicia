using Backend.Dtos;
using Backend.Models;
using KiDelicia.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace KiDelicia.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public async Task<IActionResult> Login([FromBody] AdministradorLoginDto adminLogin)
        {
            if (string.IsNullOrWhiteSpace(adminLogin.Email) || string.IsNullOrWhiteSpace(adminLogin.Senha))
                return BadRequest(new { erro = "Email e senha são obrigatórios." });

            var valido = await _adminRepository.ValidarLogin(adminLogin.Email, adminLogin.Senha);
            if (!valido)
                return Unauthorized(new { erro="Credenciais inválidas." });

            var admin = await _adminRepository.GetByEmail(adminLogin.Email);
            var token = _tokenService.GenerateToken(admin!);

            return Ok(new AdministradorResponseDto
            {
                Id = admin.Id,
                Nome = admin.Nome,
                Email = admin.Email,
                Token = token
            });
        }


        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllAdministradores()
        {
            var admins = await _adminRepository.GetAllAdministradores();

            if (admins is null || !admins.Any())
                return NotFound(new { erro = "Nenhum administrador encontrado." });

            return Ok(admins);
        }


        
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAdministradorById(int id)
        {
            var admin = await _adminRepository.GetAdministradorById(id);
            if (admin is null)
                return NotFound(new { erro = "Administrador não encontrado." });

            return Ok(admin);
        }

        
        [HttpPost]
        [Authorize]
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
                return StatusCode(500, new { erro = $"Erro ao criar administrador: {ex.Message}" });
            }
        }

        
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAdministrador(int id, [FromBody] Administrador admin)
        {
            if (id != admin.Id)
                return BadRequest(new { erro = "O ID informado não corresponde ao administrador enviado." });

            var adminExistente = await _adminRepository.GetAdministradorById(id);
            if (adminExistente is null)
                return NotFound(new { erro = "Administrador não encontrado para atualização." });

            try
            {   
                adminExistente.Nome = admin.Nome;
                adminExistente.Email = admin.Email;
                adminExistente.Senha = admin.Senha;

                await _adminRepository.UpdateAdministrador(adminExistente);
                return Ok(new { menssagem = "Administrador atualizado com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = $"Erro ao atualizar administrador: {ex.Message}" });
            }
        }

        
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAdministrador(int id)
        {
            var adminExistente = await _adminRepository.GetAdministradorById(id);
            if (adminExistente is null)
                return NotFound(new { erro = "Administrador não encontrado para exclusão." });

            try
            {
                await _adminRepository.DeleteAdministrador(id);
                return Ok(new { menssagem = "Administrador excluído com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = $"Erro ao excluir administrador: {ex.Message}" });
            }
        }
    }
}
