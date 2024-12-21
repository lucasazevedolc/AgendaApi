namespace AgendaApi.Model
{
    public class Paciente
    {
        public int PacienteId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string Cpf { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; }
    }
}
