using Backend.DTO;
using Entities.Entities;

namespace Backend.Services.Interfaces
{
    public interface IPedidoService
    {
        bool Agregar(PedidoDTO entity);
        bool Editar(PedidoDTO entity);
        bool Eliminar(PedidoDTO entity);
        PedidoDTO Obtener(int id);
        List<PedidoDTO> Obtener();
    }
}
