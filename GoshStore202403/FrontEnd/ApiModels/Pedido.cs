namespace FrontEnd.ApiModels
{
    public class Pedido
    {
        public int IdPedido { get; set; }

        public int? IdUsuario { get; set; }

        public DateTime? FechaPedido { get; set; }

        public string? Estado { get; set; }
    }
}
