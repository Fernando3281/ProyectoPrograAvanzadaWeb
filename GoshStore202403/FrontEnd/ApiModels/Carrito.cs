namespace FrontEnd.ApiModels
{
    public class Carrito
    {
        public int IdCarrito { get; set; }
        public int IdProducto { get; set; }
        public string? IdUsuario { get; set; }
        public int Cantidad { get; set; }
    }
}
