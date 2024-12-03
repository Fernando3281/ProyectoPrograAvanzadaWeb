using Backend.DTO;
namespace Backend.Services.Interfaces
{
    public interface IDireccioneService
    {
        List<DireccioneDTO> GetDireccione();
        bool Add(DireccioneDTO direccione);
        bool Update(DireccioneDTO direccione);
        bool Delete(DireccioneDTO direccioneDTO);
        DireccioneDTO GetByID(int id);

    }
}
