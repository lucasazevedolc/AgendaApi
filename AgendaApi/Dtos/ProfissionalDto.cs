using System.ComponentModel;

namespace AgendaApi.Dtos
{
    public class ProfissionalDto
    {
        public int Id { get; set; }
        [DefaultValue("")]
        public string Nome { get; set; } = string.Empty;
        [DefaultValue("")]
        public string Profissao { get; set; } = string.Empty;
        [DefaultValue("")]
        public string? RegistroProfissional { get; set; } = string.Empty;
        [DefaultValue("")]
        public string? Phone {  get; set; } = string.Empty;
        [DefaultValue("")]
        public string? Email { get; set; } = string.Empty;
        [DefaultValue("")]
        public string FirstName { get; set; } = string.Empty;
    }
}
