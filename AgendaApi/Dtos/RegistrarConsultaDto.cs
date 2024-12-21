using System.ComponentModel;

namespace AgendaApi.Dtos
{
    public class RegistrarConsultaDto
    {
        [DefaultValue("cpf")]
        public string InfoPaciente  { get; set; }
        public string InfoProfissional  { get; set; }
        [DefaultValue("mm/dd/aaaa hh:min")]
        public DateTime DataConsulta  { get; set; }
        public string? Descricao { get; set; } = string.Empty;

    }
}
