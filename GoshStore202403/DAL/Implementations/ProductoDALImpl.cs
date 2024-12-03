using DAL.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class ProductoDALImpl: DALGenericoImpl<Producto>, IProductoDAL
    {
        DbGoshStoreContext _context;

        public ProductoDALImpl(DbGoshStoreContext dbGoshStoreContext): base(dbGoshStoreContext) 
        {
            this._context = dbGoshStoreContext;
        }

    }
}
