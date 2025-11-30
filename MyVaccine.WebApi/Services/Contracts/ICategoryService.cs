using MyVaccine.WebApi.Dtos;

namespace MyVaccine.WebApi.Services.Contracts
{
    public interface ICategoryService
    {
        Task<List<VaccineCategoryResponseDto>> GetAllAsync();
        Task<VaccineCategoryResponseDto> GetByIdAsync(int id);
        Task<VaccineCategoryResponseDto> AddAsync(VaccineCategoryRequestDto request);
        Task UpdateAsync(int id, VaccineCategoryRequestDto request);
        Task DeleteAsync(int id);
    }
}
