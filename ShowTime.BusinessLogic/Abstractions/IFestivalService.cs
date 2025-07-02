using ShowTime.BusinessLogic.Dto.FestivalDto;
using ShowTime.BusinessLogic.Dto.LineupDto;

namespace ShowTime.BusinessLogic.Abstractions;

public interface IFestivalService
{
    Task<IList<FestivalGetDto>> GetAllFestivalsAsync();
    Task<FestivalGetDto?> GetFestivalByIdAsync(int id);
    Task AddFestivalAsync(FestivalCreateDto festivalCreateDto);
    Task UpdateFestivalAsync(int id, FestivalCreateDto festivalCreateDto);
    Task DeleteFestivalAsync(int id);
    Task<List<LineupGetDto>?> GetLineupsForFestival(int festivalId);
    Task<List<FullFestivalGetDto>> GetAllFestivalsFullAsync();
}