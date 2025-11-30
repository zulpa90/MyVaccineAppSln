using MyVaccine.WebApi.Dtos;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;
using MyVaccine.WebApi.Repositories.Implementations;
using MyVaccine.WebApi.Services.Contracts;

namespace MyVaccine.WebApi.Services.Implementations
{
    public class VaccineService : IVaccineService
    {
        private readonly IVaccineRepository _vaccineRepository;
        private readonly ICategoryRepository _categoryRepository;

        public VaccineService(IVaccineRepository vaccineRepository, ICategoryRepository categoryRepository)
        {
            _vaccineRepository = vaccineRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<VaccineResponseDto>> GetAllAsync()
        {
            var vaccines = await _vaccineRepository.GetAllWithCategoriesAsync();
            return vaccines.Select(v => new VaccineResponseDto
            {
                VaccineId = v.VaccineId,
                Name = v.Name,
                RequiresBooster = v.RequiresBooster,
                CategoryNames = v.Categories.Select(c => c.Name).ToList()
            }).ToList();
        }

        public async Task<VaccineResponseDto> AddAsync(VaccineRequestDto request)
        {
            var categories = await _categoryRepository.GetAllAsync(
        c => request.CategoryIds.Contains(c.VaccineCategoryId));

            if (categories == null || !categories.Any())
            {
                throw new KeyNotFoundException("One or more category IDs were not found.");
            }

            var vaccine = new Vaccine
            {
                Name = request.Name,
                RequiresBooster = request.RequiresBooster,
                Categories = categories.ToList()
            };

            var addedVaccine = await _vaccineRepository.AddAsync(vaccine);

            var response = new VaccineResponseDto
            {
                VaccineId = addedVaccine.VaccineId,
                Name = addedVaccine.Name,
                RequiresBooster = addedVaccine.RequiresBooster,
                CategoryNames = addedVaccine.Categories.Select(c => c.Name).ToList()
            };

            return response;
        }

        public async Task<VaccineResponseDto> GetByIdAsync(int id)
        {
            var vaccine = await _vaccineRepository.GetSingleAsync(c => c.VaccineId == id);

            if (vaccine == null)
            {
                return null;
            }

            return new VaccineResponseDto
            {
                VaccineId = vaccine.VaccineId,
                Name = vaccine.Name
            };
        }

        public async Task UpdateAsync(int id, VaccineRequestDto request)
        {
            var vaccineToUpdate = await _vaccineRepository.GetSingleAsync(c => c.VaccineId == id);
            if (vaccineToUpdate == null)
            {
                throw new KeyNotFoundException($"Vaccine with ID {id} not found.");
            }

            vaccineToUpdate.Name = request.Name;

            await _vaccineRepository.UpdateAsync(vaccineToUpdate);
        }
        public async Task DeleteAsync(int id)
        {
            var vaccineToDelete = await _vaccineRepository.GetSingleAsync(c => c.VaccineId == id);
            if (vaccineToDelete == null)
            {
                return;
            }
            await _vaccineRepository.DeleteAsync(vaccineToDelete);
        }
    }
}