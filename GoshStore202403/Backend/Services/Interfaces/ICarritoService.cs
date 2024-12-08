using Backend.DTO;

namespace Backend.Services.Interfaces
{
    public interface ICarritoService
    {
        Task<IEnumerable<CarritoDTO>> VerCarritoAsync(string usuarioId);
        Task<bool> AgregarProductoAsync(CarritoDTO producto);
        Task<bool> EliminarProductoAsync(int idProducto, string usuarioId);
        Task<bool> LimpiarCarritoAsync(string usuarioId);
    }
}
