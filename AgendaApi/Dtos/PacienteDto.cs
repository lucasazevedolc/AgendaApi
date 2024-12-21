using System.ComponentModel;

namespace AgendaApi.Dtos
{
    public class PacienteDto
    {
        [DefaultValue("")]
        public string Name { get; set; }
        [DefaultValue("")]
        public string? Email { get; set; }
        [DefaultValue("")]
        public string? Phone { get; set; }
        [DefaultValue("")]
        public string? Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
