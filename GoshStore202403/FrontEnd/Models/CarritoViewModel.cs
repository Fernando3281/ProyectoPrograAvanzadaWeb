namespace FrontEnd.Models
{
    public class CarritoViewModel
    {
        public int IdCarrito { get; set; }       
        public int IdProducto { get; set; }
        public string? IdUsuario { get; set; }
        public string NombreProducto { get; set; } 
        public decimal Precio { get; set; }      
        public int Cantidad { get; set; }        
        public decimal Subtotal
        {
            get { return Precio * Cantidad; }    
        }

    }
}
