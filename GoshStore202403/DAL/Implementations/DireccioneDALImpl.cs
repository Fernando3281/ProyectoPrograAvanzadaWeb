using DAL.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class DireccioneDALImpl: DALGenericoImpl<Direccione>, IDireccioneDAL
    {
        DbGoshStoreContext context;

        public DireccioneDALImpl(DbGoshStoreContext dbGoshStoreContext):base (dbGoshStoreContext) 
        {
            this.context = dbGoshStoreContext;
        }
    }
}
