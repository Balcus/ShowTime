using ShowTime.BusinessLogic.Dto.ArtistDto;
using ShowTime.BusinessLogic.Dto.FestivalDto;
using ShowTime.BusinessLogic.Dto.LineupDto;
using ShowTime.DataAccess.Models;

namespace ShowTime.BusinessLogic.Abstractions;

public interface IFestivalService
{
    Task<IList<FestivalGetDto>> GetAllFestivalsAsync();
    Task<FestivalGetDto?> GetFestivalByIdAsync(int id);
    Task AddFestivalAsync(FestivalCreateDto festivalCreateDto);
    Task UpdateFestivalAsync(int id, FestivalCreateDto festivalCreateDto);
    Task DeleteFestivalAsync(int id);
    Task<List<LineupGetDto>> GetLineupsForFestivalAsync(int festivalId);
    Task<List<ArtistGetDto>> GetArtistsForFestivalAsync(int festivalId);
    Task<List<FullFestivalDto>> GetAllFestivalsFullAsync();
    Task UpdateFestivalArtistsAsync(int festivalId, List<ArtistGetDto> updatedArtists);
    Task AddFestivalLineupAsync(int festivalId, LineupGetDto lineupDto);
}