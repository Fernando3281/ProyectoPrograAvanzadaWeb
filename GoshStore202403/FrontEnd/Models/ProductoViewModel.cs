using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FrontEnd.Models
{
    public class ProductoViewModel
    {
        public int IdProducto { get; set; }

        public string? NombreProducto { get; set; }

        public string? Descripcion { get; set; }

        public decimal? Precio { get; set; }

        public int? Stock { get; set; }

        public int? CategoriaId { get; set; }

        [BindNever] public IEnumerable<CategoriaViewModel>? Categorias { get; set; }

        public string? categoryName { get; set; }

        public string? Imagen { get; set; }

        public string? Talla { get; set; }


    }
}
