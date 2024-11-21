using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;

namespace FrontEnd.Models
{
    public class ProductoViewModel
    {
        public int IdProducto { get; set; }

        [DisplayName ("Nombre del Producto")]
        public string? NombreProducto { get; set; }
        [DisplayName("Descripción del Producto")]

        public string? Descripcion { get; set; }

        public decimal? Precio { get; set; }

        [DisplayName("Cantidad en Stock")]
        public int? Stock { get; set; }

        public int? CategoriaId { get; set; }

        [BindNever] public IEnumerable<CategoriaViewModel>? Categorias { get; set; }

        public string? categoryName { get; set; }

        [DisplayName("Link de imagen")]
        public string? Imagen { get; set; }

        public string? Talla { get; set; }


    }
}
