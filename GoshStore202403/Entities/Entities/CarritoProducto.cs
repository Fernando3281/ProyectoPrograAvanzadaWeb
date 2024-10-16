﻿using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class CarritoProducto
{
    public int IdCarrito { get; set; }

    public int IdProducto { get; set; }

    public int IdUsuario { get; set; }

    public int? Cantidad { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
