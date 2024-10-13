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
       
        private DbGoshStoreContext _dbGoshStoreContext;

        public UnidadDeTrabajo(DbGoshStoreContext dbGoshStoreContext,
                        IUsuarioDAL usuarioDAL
                       
            ) 
        {
                this._dbGoshStoreContext = dbGoshStoreContext;
                this.UsuarioDAL = usuarioDAL; 
                
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