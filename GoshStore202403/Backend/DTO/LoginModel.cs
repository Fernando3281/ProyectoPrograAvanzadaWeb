using System.ComponentModel.DataAnnotations;

namespace Backend.DTO
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Nombre de usuario es requerido")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Contraseña es requerida")]
        public string Password { get; set; }

        public TokenModel? Token { get; set; }

        public List<string>? Roles { get; set; }
    }
}
