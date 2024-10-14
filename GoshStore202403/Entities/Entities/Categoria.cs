﻿using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string? NombreCategoria { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}