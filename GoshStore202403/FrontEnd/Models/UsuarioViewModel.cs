using System.ComponentModel;

namespace FrontEnd.Models
{
    public class UsuarioViewModel
    {
        [DisplayName("ID")]
        public int IdUsuario { get; set; }

        [DisplayName("Nombre")]
        public string Nombre { get; set; } = null!;

        [DisplayName("Correo Electrónico")]
        public string Correo { get; set; } = null!;
    }
}
