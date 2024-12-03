using DAL.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class UnidadDeTrabajo : IUnidadDeTrabajo
    {
        public IUsuarioDAL UsuarioDAL { get; set; }

        public ICategoriaDAL CategoriaDAL { get; set; }

        public IDireccioneDAL DireccioneDAL { get; set; }

        public IProductoDAL ProductoDAL { get; set; }
        
        public IPedidoDAL PedidoDAL { get; set; }

        public IDetallePedidoDAL DetallePedidoDAL { get; set; }


        private DbGoshStoreContext _dbGoshStoreContext;

        public UnidadDeTrabajo(DbGoshStoreContext dbGoshStoreContext,
                        IUsuarioDAL usuarioDAL, ICategoriaDAL categoriaDAL, IDireccioneDAL direccioneDAL, IProductoDAL productoDAL, IPedidoDAL pedidoDAL,

           IDetallePedidoDAL detallePedidoDAL) 
        {
                this._dbGoshStoreContext = dbGoshStoreContext;
                this.UsuarioDAL = usuarioDAL;
                this.CategoriaDAL = categoriaDAL;
                this.DireccioneDAL = direccioneDAL;
                this.ProductoDAL = productoDAL;
                this.PedidoDAL = pedidoDAL;
            this.DetallePedidoDAL = detallePedidoDAL;
        }
       

        public bool Complete()
        {
            try
            {
                _dbGoshStoreContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public void Dispose()
            {
            this._dbGoshStoreContext.Dispose();
        }
    }
}