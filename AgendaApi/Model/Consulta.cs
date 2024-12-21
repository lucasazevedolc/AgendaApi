using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AgendaApi.Model
{
    public class Consulta
    {
        public int ConsultaId { get; set; }
        public DateTime DataConsulta { get; set; }
        public string? Observacoes { get; set; } = string.Empty;

        public string? NomePaciente { get; set; } = string.Empty;
        public string? CpfPaciente { get; set; } = string.Empty;

        public string? NomeProfissional { get; set; } = string.Empty;
        public string? PrimeiroNomeProfissional { get; set; } = string.Empty;
        public string? RegistroProfissional { get; set; } = string.Empty;
    }
}
