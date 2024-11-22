using DAL.Interfaces;
using Entities.Entities;
using System;

namespace DAL.Implementations
{
    public class DetallePedidoDALImpl : DALGenericoImpl<DetallePedido>, IDetallePedidoDAL
    {
        DbGoshStoreContext context;

        public DetallePedidoDALImpl(DbGoshStoreContext context) : base(context)
        {
            this.context = context;
        }
    }
}

