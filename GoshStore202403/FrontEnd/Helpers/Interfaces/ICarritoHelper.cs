using FrontEnd.Models;

namespace FrontEnd.Helpers.Interfaces
{
    public interface ICarritoHelper
    {
        Task<List<CarritoViewModel>> ObtenerCarritoAsync(string usuarioId);

        Task<bool> AgregarAlCarritoAsync(CarritoViewModel item);

        Task<bool> EliminarDelCarritoAsync(int idCarrito, int idProducto);

        Task<bool> VaciarCarritoAsync(string usuarioId);
    }
}
