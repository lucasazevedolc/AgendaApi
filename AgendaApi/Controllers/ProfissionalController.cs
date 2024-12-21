using AgendaApi.Dtos;
using AgendaApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgendaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfissionalController : ControllerBase
    {
        private readonly ApiDbContext _apiDbContext;
        public ProfissionalController(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        [Authorize]
        [HttpPost("adicionar")]
        public async Task<IActionResult> AddProfissional(string name, string? email, string? phone, string profissao, string? registroprofissional, string firstname)
        {
            var profissional = new Profissional();
            profissional.Name = name;
            profissional.Email = email;
            profissional.Phone = phone;
            profissional.Profissao = profissao;
            profissional.RegistroProfissional = registroprofissional;
            profissional.FirstName = firstname;
            _apiDbContext.Profissionais.Add(profissional);
            await _apiDbContext.SaveChangesAsync();
            return Ok(profissional);
        }

        [Authorize]
        [HttpGet("busca/{info}")]
        public async Task<IActionResult> BuscaProfissional(string info)
        {
            var profissional = await _apiDbContext.Profissionais.FirstOrDefaultAsync(p => p.Name == info || p.RegistroProfissional == info || p.Profissao == info || p.Phone == info ||p.Email == info || p.FirstName == info);
            if (profissional == null) return BadRequest("Profissional não encontrado"); 
            return Ok(profissional);
        }

        [Authorize]
        [HttpGet("listar")]
        public async Task<IActionResult> ListarProfissionais()
        {
            var profissionais = await _apiDbContext.Profissionais
                .OrderBy(p => p.Name)
                .Select(p => new ProfissionalDto
                {
                    Id = p.ProfissionalId,
                    Nome = p.Name,
                    Profissao = p.Profissao,
                    RegistroProfissional = p.RegistroProfissional
                })
                .ToListAsync();
            return Ok(profissionais);
        }

        [Authorize]
        [HttpPut("editar")]
        public async Task<IActionResult> EditarPaciente([FromQuery][FromForm] string nomecompleto, [FromBody] ProfissionalDto profissionaleditado)
        {
            if (string.IsNullOrWhiteSpace(nomecompleto)) return BadRequest("O campo é obrigatório");

            var profissional = await _apiDbContext.Profissionais.FirstOrDefaultAsync(p => p.Name == nomecompleto);
            if (profissional == null) return NotFound($"Profissional com o nome {nomecompleto}', não foi encontrado");
            if (!string.IsNullOrWhiteSpace(profissionaleditado.Nome)) profissional.Name = profissionaleditado.Nome;
            if (!string.IsNullOrWhiteSpace(profissionaleditado.Profissao)) profissional.Profissao = profissionaleditado.Profissao;
            if (!string.IsNullOrWhiteSpace(profissionaleditado.RegistroProfissional)) profissional.RegistroProfissional = profissionaleditado.RegistroProfissional;
            if (!string.IsNullOrWhiteSpace(profissionaleditado.Email)) profissional.Email = profissionaleditado.Email;
            if (!string.IsNullOrWhiteSpace(profissionaleditado.Phone)) profissional.Phone = profissionaleditado.Phone;
            if (!string.IsNullOrWhiteSpace(profissionaleditado.FirstName)) profissional.FirstName = profissionaleditado.FirstName;

            try
            {
                await _apiDbContext.SaveChangesAsync();
                return Ok("Dados do profissional foram atualizados com sucesso");

            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Erro ao salvar mudanças");
            }
        }

        [Authorize]
        [HttpDelete("deletar")]
        public async Task<IActionResult> ApagarProfissional([FromQuery] string info)
        {
            if (string.IsNullOrEmpty(info)) return BadRequest("O campo é obrigatório");
            var profissional = await _apiDbContext.Profissionais.FirstOrDefaultAsync(p => p.Name == info || p.RegistroProfissional == info || p.Profissao == info || p.Phone == info || p.Email == info || p.FirstName == info);
            if (profissional == null) return NotFound("Profissional não encontrado");
            _apiDbContext.Profissionais.Remove(profissional);
            await _apiDbContext.SaveChangesAsync();
            return Ok("Profissional deletado com sucesso");
        }
    }
}
