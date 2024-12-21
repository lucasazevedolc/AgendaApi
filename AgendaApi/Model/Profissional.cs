namespace AgendaApi.Model
{
    public class Profissional
    {
        public int ProfissionalId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Profissao { get; set; } = string.Empty;
        public string? RegistroProfissional { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;
    }
}
