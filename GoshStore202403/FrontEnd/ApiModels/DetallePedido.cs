namespace FrontEnd.ApiModels
{
    public class DetallePedido
    {
        public int IdDetallePedido { get; set; } // Identificador único del detalle del pedido
        public int? IdPedido { get; set; }       // Identificador del pedido al que pertenece
        public int? IdProducto { get; set; }    // Identificador del producto
        public decimal? PrecioUnitario { get; set; } // Precio por unidad del producto
        public int? Cantidad { get; set; }      // Cantidad del producto solicitada
    }
}
