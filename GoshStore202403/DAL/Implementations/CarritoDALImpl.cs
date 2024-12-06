using DAL.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations
{
    public class CarritoDALImpl : ICarritoDAL
    {
        DbGoshStoreContext _context;

        public CarritoDALImpl(DbGoshStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CarritoProducto>> VerCarritoAsync(string usuarioId)
        {
            return await _context.CarritoProductos
                .FromSqlInterpolated($"EXEC VerCarrito @UsuarioId = {usuarioId}")
                .ToListAsync();
        }

        public async Task<bool> AgregarProductoAsync(CarritoProducto producto)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync(
                    $"EXEC AgregarAlCarrito @UsuarioId = {producto.IdUsuario}, @id_producto = {producto.IdProducto}, @cantidad = {producto.Cantidad}");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EliminarProductoAsync(int idProducto, string usuarioId)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync(
                    $"EXEC EliminarArticuloDelCarrito @UsuarioId = {usuarioId}, @id_producto = {idProducto}");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> LimpiarCarritoAsync(string usuarioId)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync(
                    $"EXEC LimpiarCarrito @UsuarioId = {usuarioId}");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
