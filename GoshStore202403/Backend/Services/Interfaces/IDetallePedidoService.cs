using Backend.DTO;
using Entities.Entities;
using FrontEnd.Models;

namespace Backend.Services.Interfaces
{
    public interface IDetallePedidoService
    {
        bool Agregar(DetallePedidoDTO entity);
        bool Editar(DetallePedidoDTO entity);
        bool Eliminar(DetallePedidoDTO entity);
        DetallePedidoDTO Obtener(int id);
        List<DetallePedidoDTO> Obtener();
    }
}
