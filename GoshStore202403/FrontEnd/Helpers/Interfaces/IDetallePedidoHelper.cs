using FrontEnd.Models;

namespace FrontEnd.Helpers.Interfaces
{
    public interface IDetallePedidoHelper
    {
        List<DetallePedidoViewModel> GetDetallesPedido(); // Obtener todos los detalles de pedidos
        DetallePedidoViewModel GetDetallePedido(int id);  // Obtener un detalle de pedido específico por su ID
        DetallePedidoViewModel Add(DetallePedidoViewModel detalle); // Agregar un nuevo detalle de pedido
        DetallePedidoViewModel Update(int id, DetallePedidoViewModel detalle); // Actualizar un detalle de pedido existente
        void Delete(int id); // Eliminar un detalle de pedido por su ID
    }
}
