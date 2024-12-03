using FrontEnd.Models;

namespace FrontEnd.Helpers.Interfaces
{
    public interface ICategoriaHelper
    {
        List<CategoriaViewModel> GetAll();
        CategoriaViewModel GetById(int id);
    }
}
