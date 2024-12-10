namespace FrontEnd.Models
{
    public class DetallePedidoViewModel
    {
        public int IdDetallePedido { get; set; }
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }
        public string? NombreProducto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal Total => PrecioUnitario * Cantidad;
    }
}
