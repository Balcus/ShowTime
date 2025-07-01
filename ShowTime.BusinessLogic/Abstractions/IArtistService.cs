using ShowTime.BusinessLogic.Dto;
using ShowTime.BusinessLogic.Dto.ArtistDto;
using ShowTime.DataAccess.Models;

namespace ShowTime.BusinessLogic.Abstractions;

public interface IArtistService
{
    Task<IList<ArtistGetDto>> GetAllArtistsAsync();
    Task<ArtistGetDto?> GetArtistByIdAsync(int id);
    Task AddArtistAsync(ArtistCreateDto artistCreateDto);
    Task UpdateArtistAsync(int id, ArtistCreateDto artist);
    Task DeleteArtistAsync(int id);
}