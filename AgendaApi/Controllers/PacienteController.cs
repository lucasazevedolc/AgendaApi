using AgendaApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgendaApi.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace AgendaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly ApiDbContext _apiDbContext;
        public PacienteController(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        [Authorize]
        [HttpPost("adicionar")]
        public async Task<IActionResult> AddPaciente(string name, string? email, string? phone, string cpf, DateTime datanascimento)
        {
            var paciente = new Paciente();
                paciente.Name = name;
                paciente.Email = email;
                paciente.Phone = phone;
                paciente.Cpf = cpf;
                paciente.DataNascimento = datanascimento;

            _apiDbContext.Pacientes.Add(paciente);
            await _apiDbContext.SaveChangesAsync();
            return Ok(paciente);
        }

        [Authorize]
        [HttpPut("editar")]
        public async Task<IActionResult> EditarPaciente([FromQuery][FromForm] string cpf, [FromBody] PacienteDto pacienteeditado)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return BadRequest("O cpf é obrigatório");

            var paciente = await _apiDbContext.Pacientes.FirstOrDefaultAsync(p => p.Cpf == cpf);
            if (paciente == null) return NotFound($"Paciente com o cpf {cpf}', não foi encontrado");
            if (!string.IsNullOrWhiteSpace(pacienteeditado.Name)) paciente.Name = pacienteeditado.Name;
            if (!string.IsNullOrWhiteSpace(pacienteeditado.Cpf)) paciente.Cpf = pacienteeditado.Cpf;
            if (!string.IsNullOrWhiteSpace(pacienteeditado.Email)) paciente.Email = pacienteeditado.Email;
            if (!string.IsNullOrWhiteSpace(pacienteeditado.Phone)) paciente.Phone = pacienteeditado.Phone;
            if (pacienteeditado.DataNascimento != default(DateTime)) paciente.DataNascimento = pacienteeditado.DataNascimento;

            try
            {
                await _apiDbContext.SaveChangesAsync();
                return Ok("Dados do paciente foram atualizados com sucesso");

            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Erro ao salvar mudanças");
            }
        }

        [Authorize]
        [HttpGet("busca/{cpf}")]
        public async Task<IActionResult> BuscaPorCpf(string cpf)
        {
            var paciente = await _apiDbContext.Pacientes.FirstOrDefaultAsync(p => p.Cpf == cpf);
            if (paciente == null) return BadRequest("Cpf não encontrado");
            return Ok(paciente);
        }

        [Authorize]
        [HttpGet("listar")]
        public async Task<IActionResult> ListarPacientes([FromQuery] int pagina = 1)
        {
            const int TamanhoPagina = 10;

            if (pagina < 1) return BadRequest("Número da página deve ser maior que 0.");
            int totalRegistros = await _apiDbContext.Pacientes.CountAsync();
            int totalPaginas = (int)Math.Ceiling(totalRegistros / (double)TamanhoPagina);
            if (pagina > totalPaginas && totalRegistros > 0) return BadRequest("Página solicitada não existe");

            var pacientes = await _apiDbContext.Pacientes
                .OrderBy(p => p.Name)
                .Skip((pagina - 1) * TamanhoPagina)
                .Take(TamanhoPagina)
                .Select(p => new PacienteDto
                {
                    Name = p.Name ?? "Nome não informado",
                    Cpf = p.Cpf ?? "Cpf não informado",
                    Email = p.Email ?? "Email não informado",
                    Phone = p.Phone ?? "Celular não informado",
                    DataNascimento = p.DataNascimento.HasValue ? p.DataNascimento.Value : DateTime.Now
                }).ToListAsync();

            return Ok(new
            {
                PaginaAtual = pagina,
                totalPaginas = totalPaginas,
                totalRegistros = totalRegistros,
                Pacientes = pacientes
            });
        }

        [Authorize]
        [HttpDelete("deletar")]
        public async Task<IActionResult> ApagarPaciente([FromQuery] string cpf)
        {
            if (string.IsNullOrEmpty(cpf)) return BadRequest("O cpf é obrigatório");
            var paciente = await _apiDbContext.Pacientes.FirstOrDefaultAsync(p => p.Cpf == cpf);
            if (paciente == null) return NotFound($"Paciente com o cpf '{cpf}' não foi encontrado");
            _apiDbContext.Pacientes.Remove(paciente);
            await _apiDbContext.SaveChangesAsync();
            return Ok("Paciente deletado com sucesso");
        }

    }
}