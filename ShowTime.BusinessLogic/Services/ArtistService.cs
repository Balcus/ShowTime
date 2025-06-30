using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.Dto;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Interfaces;

namespace ShowTime.BusinessLogic.Services;

public class ArtistService(IRepository<Artist> repository) : IArtistService
{
    public async Task<IList<ArtistGetDto>> GetAllArtistsAsync()
    {
        try
        {
            var artists = await repository.GetAllAsync();
            return artists.Select(artist => new ArtistGetDto
            {
                Id = artist.Id,
                Name = artist.Name,
                Genre = artist.Genre,
                Image = artist.Image,
            }).ToList();
        }
        catch (Exception e)
        {
            throw new Exception($"An error occured while retrieving all artists: {e.Message}");
        }
    }

    public async Task<ArtistGetDto?> GetArtistByIdAsync(int id)
    {
        try
        {
            var artist = await repository.GetByIdAsync(id);
            if (artist != null)
            {
                return new ArtistGetDto()
                {
                    Id = artist.Id,
                    Name = artist.Name,
                    Genre = artist.Genre,
                    Image = artist.Image,
                };
            }

            return null;
        }
        catch (Exception e)
        {
            throw new Exception($"An error occured while retrieving artist with id {id} : {e.Message}");
        }
    }

    public async Task AddArtistAsync(ArtistCreateDto artistCreateDto)
    {
        try
        {
            var artist = new Artist()
            {
                Name = artistCreateDto.Name,
                Image = artistCreateDto.Image,
                Genre = artistCreateDto.Genre,
            };
            
            await repository.CreateAsync(artist);
        }
        catch (Exception e)
        {
            throw new Exception($"An error occured while adding artist: {e.Message}");
        }
    }

    public async Task UpdateArtistAsync(int id, ArtistCreateDto artistCreateDto)
    {
        try
        {
            var artist = await repository.GetByIdAsync(id);
            if (artist != null)
            {
                artist.Name = artistCreateDto.Name;
                artist.Image = artistCreateDto.Image;
                artist.Genre = artistCreateDto.Genre;
                await repository.UpdateAsync(artist);
            }
        }
        catch (Exception e)
        {
            throw new Exception($"An error occured while updating artist with id: {id} : {e.Message}");
        }
    }

    public async Task DeleteArtistAsync(int id)
    {
        try
        {
            await repository.DeleteAsync(id);
        }
        catch (Exception e)
        {
            throw new Exception($"An error occured while deleting artist with id {id}: {e.Message}");
        }

    }
}