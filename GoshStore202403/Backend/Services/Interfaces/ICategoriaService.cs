using Backend.DTO;
namespace Backend.Services.Interfaces
{
    public interface ICategoriaService
    {
        List<CategoriaDTO> GetCategorias();
        CategoriaDTO GetByID(int id);

    }
}
