using AgendaApi.Dtos;
using AgendaApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AgendaApi.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class ConsultaController : ControllerBase
    {
        private readonly ApiDbContext _apiDbContext;
        public ConsultaController(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        [Authorize]
        [HttpPost("api/Consulta/adicionar")]
        public async Task<IActionResult> AdicionarConsulta(RegistrarConsultaDto consulta)
        {
            var profissional = await _apiDbContext.Profissionais.FirstOrDefaultAsync(p => p.Name == consulta.InfoProfissional || p.RegistroProfissional == consulta.InfoProfissional || p.Profissao == consulta.InfoProfissional || p.Phone == consulta.InfoProfissional || p.Email == consulta.InfoProfissional || p.FirstName == consulta.InfoProfissional);
            if (profissional == null) return NotFound("Profissional não encontrado");
            var registroProfissional = profissional.RegistroProfissional;
            var nomeProfissional = profissional.Name;
            var primeironomeProfissional = profissional.FirstName;
            var paciente = await _apiDbContext.Pacientes.FirstOrDefaultAsync(p => p.Cpf == consulta.InfoPaciente);
            if (paciente == null) return NotFound("Paciente não encontrado");
            var cpfPaciente = paciente.Cpf;
            var nomePaciente = paciente.Name;

            var novaConsulta = new Consulta
            {
                NomePaciente = nomePaciente,
                CpfPaciente = cpfPaciente,
                NomeProfissional = nomeProfissional,
                RegistroProfissional = registroProfissional,
                DataConsulta = consulta.DataConsulta,
                Observacoes = consulta.Descricao,
                PrimeiroNomeProfissional = primeironomeProfissional
            };

            _apiDbContext.Consultas.Add(novaConsulta);
            await _apiDbContext.SaveChangesAsync();

            return Ok(novaConsulta);
        }

        [Authorize]
        [HttpPut("api/consulta/editar")]
        public async Task<IActionResult> EditarConsulta(int? idConsulta, string? infocpf, ConsultaDto AtualizarConsulta)
        {
            var consulta = await _apiDbContext.Consultas.FindAsync(idConsulta);
            if (idConsulta == null)
            {
                consulta = await _apiDbContext.Consultas.FirstOrDefaultAsync(c => c.CpfPaciente == infocpf);
            }
            if (consulta == null) return NotFound("Consulta não encontrada");

            if (AtualizarConsulta.DataConsulta != default(DateTime)) consulta.DataConsulta = AtualizarConsulta.DataConsulta;
            if (!string.IsNullOrWhiteSpace(AtualizarConsulta.RegistroProfissional)) consulta.RegistroProfissional = AtualizarConsulta.RegistroProfissional;
            if (!string.IsNullOrWhiteSpace(AtualizarConsulta.NomeProfissional)) consulta.NomeProfissional = AtualizarConsulta.NomeProfissional;
            if (!string.IsNullOrWhiteSpace(AtualizarConsulta.PrimeiroNome)) consulta.PrimeiroNomeProfissional = AtualizarConsulta.PrimeiroNome;
            if (!string.IsNullOrWhiteSpace(AtualizarConsulta.NomePaciente)) consulta.NomePaciente = AtualizarConsulta.NomePaciente;
            if (!string.IsNullOrWhiteSpace(AtualizarConsulta.CpfPaciente)) consulta.CpfPaciente = AtualizarConsulta.CpfPaciente;
            if (!string.IsNullOrWhiteSpace(AtualizarConsulta.Observacoes)) consulta.Observacoes = AtualizarConsulta.Observacoes;

            await _apiDbContext.SaveChangesAsync();
            return Ok(consulta);
        }

        [Authorize]
        [HttpGet("api/Consulta/busca-consulta")]
        public async Task<IActionResult> BuscarConsultas(string info)
        {
            var consultas = await _apiDbContext.Consultas
                .Where(c=> c.CpfPaciente == info || c.RegistroProfissional == info || c.NomeProfissional == info || c.NomePaciente == info || c.Observacoes == info || c.PrimeiroNomeProfissional == info)
                .ToListAsync();
            if (!consultas.Any()) return NotFound("Nenhuma consulta foi encontrada");
            
            return Ok(consultas);
        }

        [Authorize]
        [HttpGet("api/consulta/lista-dia")]
        public async Task<IActionResult> ListaDia([FromQuery] DateTime MM_DD_AAAA)
        {
            if (MM_DD_AAAA == default)
            {
                MM_DD_AAAA = DateTime.Today;
            }
            var hoje = MM_DD_AAAA;
            var diaextra = MM_DD_AAAA.AddDays(1);
            var consultas = await _apiDbContext.Consultas
                .Where(c => c.DataConsulta >= MM_DD_AAAA && c.DataConsulta < diaextra)
                .ToListAsync();
            if (!consultas.Any()) return NotFound("Nenhuma consulta foi encontrada");

            return Ok(consultas);
        }

        [Authorize]
        [HttpDelete("api/Consulta/deletar")]
        public async Task<IActionResult> DeletarConsulta(int? id, string? cpf)
        {
            if (id == null && string.IsNullOrEmpty(cpf)) return BadRequest("Consulta não encontrada");
            var consulta = await _apiDbContext.Consultas.FindAsync(id);
            if (id == null)
            {
                consulta = await _apiDbContext.Consultas.FirstOrDefaultAsync(c => c.CpfPaciente == cpf);
            }

            _apiDbContext.Remove(consulta);
            await _apiDbContext.SaveChangesAsync();

            return Ok(consulta);
        }
    }
}
