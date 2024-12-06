using Backend.DTO;
using Backend.Services.Interfaces;
using DAL;
using DAL.Interfaces;
using Entities.Entities;

namespace Backend.Services.Implementations
{
    public class CarritoService : ICarritoService
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public CarritoService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<IEnumerable<CarritoDTO>> VerCarritoAsync(string usuarioId)
        {
            var carrito = await _unidadDeTrabajo.CarritoDAL.VerCarritoAsync(usuarioId);
            return carrito.Select(c => new CarritoDTO
            {
                IdCarrito = c.IdCarrito,
                IdProducto = c.IdProducto,
                IdUsuario = c.IdUsuario,
                Cantidad = c.Cantidad
            });
        }

        public async Task<bool> AgregarProductoAsync(CarritoDTO producto)
        {
            var carritoProducto = new CarritoProducto
            {
                IdCarrito = producto.IdCarrito,
                IdProducto = producto.IdProducto,
                IdUsuario = producto.IdUsuario,
                Cantidad = producto.Cantidad
            };

            return await _unidadDeTrabajo.CarritoDAL.AgregarProductoAsync(carritoProducto);
        }

        public async Task<bool> EliminarProductoAsync(int idProducto, string usuarioId)
        {
            return await _unidadDeTrabajo.CarritoDAL.EliminarProductoAsync(idProducto, usuarioId);
        }

        public async Task<bool> LimpiarCarritoAsync(string usuarioId)
        {
            return await _unidadDeTrabajo.CarritoDAL.LimpiarCarritoAsync(usuarioId);
        }
    }
}

