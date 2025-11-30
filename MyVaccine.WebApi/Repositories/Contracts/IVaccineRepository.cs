using MyVaccine.WebApi.Models;

namespace MyVaccine.WebApi.Repositories.Contracts
{
    public interface IVaccineRepository : IBaseRepository<Vaccine>
    {
        Task<List<Vaccine>> GetAllWithCategoriesAsync();
        Task<Vaccine> GetByIdWithCategoriesAsync(int id);
    }
}
