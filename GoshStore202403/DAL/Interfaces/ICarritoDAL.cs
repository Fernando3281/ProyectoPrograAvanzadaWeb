using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICarritoDAL
    {
        Task<IEnumerable<CarritoProducto>> VerCarritoAsync(string usuarioId);
        Task<bool> AgregarProductoAsync(CarritoProducto producto);
        Task<bool> EliminarProductoAsync(int idProducto, string usuarioId);
        Task<bool> LimpiarCarritoAsync(string usuarioId);
    }
}
