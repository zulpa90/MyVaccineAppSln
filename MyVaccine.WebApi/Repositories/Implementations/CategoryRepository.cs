using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;

namespace MyVaccine.WebApi.Repositories.Implementations
{
    public class CategoryRepository : BaseRepository<VaccineCategory>, ICategoryRepository
    {
        public CategoryRepository(MyVaccineAppDbContext context) : base(context)
        {
        }
    }
}
