namespace Backend.DTO
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }

        public string? NombreProducto { get; set; }

        public string? Descripcion { get; set; }

        public decimal? Precio { get; set; }

        public int? Stock { get; set; }

        public int? CategoriaId { get; set; }

        public string? Imagen { get; set; }

        public string? Talla { get; set; }
    }
}
