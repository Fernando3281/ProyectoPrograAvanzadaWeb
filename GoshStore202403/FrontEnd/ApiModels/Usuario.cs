namespace FrontEnd.ApiModels
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string Nombre { get; set; } = null!;

        public string Correo { get; set; } = null!;
    }
}
