using Microsoft.EntityFrameworkCore;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;

namespace MyVaccine.WebApi.Repositories.Implementations
{
    public class VaccineRepository : BaseRepository<Vaccine>, IVaccineRepository
    {
        public VaccineRepository(MyVaccineAppDbContext context) : base(context)
        {
        }

        public async Task<List<Vaccine>> GetAllWithCategoriesAsync()
        {
            return await _context.Vaccines
                .Include(v => v.Categories)
                .ToListAsync();
        }

        public async Task<Vaccine> GetByIdWithCategoriesAsync(int id)
        {
            return await _context.Vaccines
                .Include(v => v.Categories)
                .FirstOrDefaultAsync(v => v.VaccineId == id);
        }
    }
}
