using MyVaccine.WebApi.Dtos;

namespace MyVaccine.WebApi.Services.Contracts
{
    public interface IVaccineService
    {
        Task<List<VaccineResponseDto>> GetAllAsync();
        Task<VaccineResponseDto> GetByIdAsync(int id);
        Task<VaccineResponseDto> AddAsync(VaccineRequestDto request);
        Task UpdateAsync(int id, VaccineRequestDto request);
        Task DeleteAsync(int id);
    }
}
