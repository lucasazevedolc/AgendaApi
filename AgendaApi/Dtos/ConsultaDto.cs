namespace AgendaApi.Dtos
{
    public class ConsultaDto
    {
        public string NomePaciente { get; set; } = string.Empty;
        public string CpfPaciente { get; set; } = string.Empty ;
        public string NomeProfissional { get; set; } = string.Empty;
        public string RegistroProfissional { get; set; } = string.Empty ;
        public string PrimeiroNome {  get; set; } = string.Empty ;
        public DateTime DataConsulta    { get; set; }
        public string Observacoes { get; set; } = string.Empty;

    }
}
