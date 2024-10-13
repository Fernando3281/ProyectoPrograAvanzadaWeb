using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Direccione
{
    public int IdDireccion { get; set; }

    public int? IdUsuario { get; set; }

    public string? DireccionEnvio { get; set; }

    public string? Ciudad { get; set; }

    public string? CodigoPostal { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
