using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Transaccione
{
    public int IdTransaccion { get; set; }

    public int? IdPedido { get; set; }

    public string? MetodoPago { get; set; }

    public decimal? MontoTotal { get; set; }

    public DateTime? FechaTransaccion { get; set; }

    public string? Estado { get; set; }

    public virtual Pedido? IdPedidoNavigation { get; set; }
}
