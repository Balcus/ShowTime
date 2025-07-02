using ShowTime.BusinessLogic.Dto.FestivalDto;

namespace ShowTime.BusinessLogic.Abstractions;

public interface IFestivalService
{
    Task<IList<FestivalGetDto>> GetAllAsync();
    Task<FestivalGetDto?> GetByIdAsync(int id);
    Task AddAsync(FestivalCreateDto artistCreateDto);
    Task UpdateAsync(int id, FestivalCreateDto artist);
    Task DeleteAsync(int id);
}