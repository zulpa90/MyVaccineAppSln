using MyVaccine.WebApi.Dtos;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;
using MyVaccine.WebApi.Services.Contracts;

namespace MyVaccine.WebApi.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<VaccineCategoryResponseDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            return categories.Select(c => new VaccineCategoryResponseDto
            {
                VaccineCategoryId = c.VaccineCategoryId,
                Name = c.Name
            }).ToList();
        }

        public async Task<VaccineCategoryResponseDto> GetByIdAsync(int id)
        {
            var category = await _repository.GetSingleAsync(c => c.VaccineCategoryId == id);

            if (category == null)
            {
                return null;
            }

            return new VaccineCategoryResponseDto
            {
                VaccineCategoryId = category.VaccineCategoryId,
                Name = category.Name
            };
        }

        public async Task<VaccineCategoryResponseDto> AddAsync(VaccineCategoryRequestDto request)
        {
            var category = new VaccineCategory
            {
                Name = request.Name
            };

            var addedCategory = await _repository.AddAsync(category);

            return new VaccineCategoryResponseDto
            {
                VaccineCategoryId = addedCategory.VaccineCategoryId,
                Name = addedCategory.Name
            };
        }

        public async Task UpdateAsync(int id, VaccineCategoryRequestDto request)
        {
            var categoryToUpdate = await _repository.GetSingleAsync(c => c.VaccineCategoryId == id);
            if (categoryToUpdate == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }

            categoryToUpdate.Name = request.Name;

            await _repository.UpdateAsync(categoryToUpdate);
        }

        public async Task DeleteAsync(int id)
        {
            var categoryToDelete = await _repository.GetSingleAsync(c => c.VaccineCategoryId == id);
            if (categoryToDelete == null)
            {
                return;
            }
            await _repository.DeleteAsync(categoryToDelete);
        }
    }
}
