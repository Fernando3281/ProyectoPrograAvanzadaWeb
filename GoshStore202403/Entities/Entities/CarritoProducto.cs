using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class CarritoProducto
{
    public int IdCarrito { get; set; } 
    public int IdProducto { get; set; } 
    public string IdUsuario { get; set; } = null!; 
    public int Cantidad { get; set; } 

    public virtual Producto IdProductoNavigation { get; set; } = null!;
    public virtual IdentityUser UsuarioNavigation { get; set; } = null!;
}

