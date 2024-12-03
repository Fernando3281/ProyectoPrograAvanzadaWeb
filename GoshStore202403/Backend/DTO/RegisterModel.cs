using System.ComponentModel.DataAnnotations;

namespace Backend.DTO
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Nombre de usuario es requerido")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Correo electrónico es requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contraseña es requerida")]
        public string Password { get; set; }
    }
}
