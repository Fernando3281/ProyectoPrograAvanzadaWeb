using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnidadDeTrabajo : IDisposable
    {

        IUsuarioDAL UsuarioDAL { get; }

        ICategoriaDAL CategoriaDAL { get; }

        IProductoDAL ProductoDAL { get; }
        
        IPedidoDAL PedidoDAL { get; }

        IDetallePedidoDAL DetallePedidoDAL { get; }


        bool Complete();

    }
}
