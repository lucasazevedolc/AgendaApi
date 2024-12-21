using System.ComponentModel;

namespace AgendaApi.Dtos
{
    public class UserLoginDto
    {
        [DefaultValue("")]
        public string Username { get; set; }
        [DefaultValue("")]
        public string Password { get; set; }
    }
}

