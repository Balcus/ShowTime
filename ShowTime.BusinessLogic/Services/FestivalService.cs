using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.Dto.ArtistDto;
using ShowTime.BusinessLogic.Dto.FestivalDto;
using ShowTime.BusinessLogic.Dto.LineupDto;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Interfaces;

namespace ShowTime.BusinessLogic.Services;

public class FestivalService(IFestivalRepository festivalRepository) : IFestivalService
{
    public async Task<List<FullFestivalDto>> GetAllFestivalsFullAsync()
    {
        try
        {
            var festivals = await festivalRepository.GetAllAsync();
            return festivals.Select(f => new FullFestivalDto
            {
                Id = f.Id,
                Name = f.Name,
                Capacity = f.Capacity,
                EndDate = f.EndDate,
                StartDate = f.StartDate,
                Location = f.Location,
                SplashArt = f.SplashArt,
                Artists = f.Artists.Select(a => new ArtistGetDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Image = a.Image,
                    Genre = a.Genre,
                }).ToList(),
                Lineups = f.Lineups.Select(l => new LineupGetDto{
                    FestivalId = l.FestivalId,
                    ArtistId = l.ArtistId,
                    StartTime = l.StartTime,
                    Stage = l.Stage,
                }).ToList()
            }).ToList();
            
        }
        catch (Exception e)
        {
            throw new Exception($"An error occured while retrieving all festivals : {e.Message}");
        }
    }

    public async Task UpdateFestivalArtistsAsync(int id, List<ArtistGetDto> updatedArtists)
    {
        var newArtists = updatedArtists
            .Select(artist => new Artist {
                Id = artist.Id,
                Name = artist.Name,
                Image = artist.Image,
                Genre = artist.Genre,
            }).ToList();

        await festivalRepository.UpdateFestivalArtistsAsync(id, newArtists);
    }

    public async Task<IList<FestivalGetDto>> GetAllFestivalsAsync()
    {
        try
        {
            var festivals = await festivalRepository.GetAllAsync();
            return festivals.Select(f => new FestivalGetDto
            {
                Id = f.Id,
                Name = f.Name,
                Capacity = f.Capacity,
                EndDate = f.EndDate,
                StartDate = f.StartDate,
                Location = f.Location,
                SplashArt = f.SplashArt,
            }).ToList();
        }
        catch (Exception e)
        {
            throw new Exception($"An error occured while retrieving all festivals : {e.Message}");
        }
    }

    public async Task<FestivalGetDto?> GetFestivalByIdAsync(int id)
    {
        try
        {
            var festival = await festivalRepository.GetByIdAsync(id);
            if (festival == null) return null;
            return new FestivalGetDto
            {
                Id = festival.Id,
                Name = festival.Name,
                Capacity = festival.Capacity,
                EndDate = festival.EndDate,
                StartDate = festival.StartDate,
                Location = festival.Location,
                SplashArt = festival.SplashArt,
            };
        }
        catch (Exception e)
        {
            throw new Exception($"An error occured while retrieving festival with id {id} : {e.Message}");
        }
    }

    public async Task AddFestivalAsync(FestivalCreateDto festivalCreateDto)
    {
        try
        {
            var newFestival = new Festival
            {
                Name = festivalCreateDto.Name,
                Capacity = festivalCreateDto.Capacity,
                EndDate = festivalCreateDto.EndDate,
                StartDate = festivalCreateDto.StartDate,
                Location = festivalCreateDto.Location,
                SplashArt = festivalCreateDto.SplashArt,
            };
            await festivalRepository.CreateAsync(newFestival);
        }
        catch (Exception e)
        {
            throw new Exception($"An error occured while adding festival: {e.Message}");
        }
    }

    public async Task UpdateFestivalAsync(int id, FestivalCreateDto festivalUpdateDro)
    {
        try
        {
            var festival = await festivalRepository.GetByIdAsync(id);
            if (festival != null)
            {
                festival.Name = festivalUpdateDro.Name;
                festival.Capacity = festivalUpdateDro.Capacity;
                festival.EndDate = festivalUpdateDro.EndDate;
                festival.StartDate = festivalUpdateDro.StartDate;
                festival.Location = festivalUpdateDro.Location;
                festival.SplashArt = festivalUpdateDro.SplashArt;
                await festivalRepository.UpdateAsync(festival);
            }
        }
        catch (Exception e)
        {
            throw new Exception($"An error occured while updating festival with id: {id} : {e.Message}");
        }
    }

    public async Task DeleteFestivalAsync(int id)
    {
        try
        {
            await festivalRepository.DeleteAsync(id);
        }
        catch (Exception e)
        {
            throw new Exception($"An error occured while deleting festival with id {id}: {e.Message}");
        }
    }

    public async Task<List<LineupGetDto>> GetLineupsForFestivalAsync(int festivalId)
    {
        try
        {
            var festival = await festivalRepository.GetByIdAsync(festivalId);
            
            if (festival == null)
            {
                throw new Exception($"Festival with id {festivalId} not found while trying to get festival lineups");   
            }
            
            return festival.Lineups.Select(l => new LineupGetDto
            {
                FestivalId = festival.Id,
                ArtistId = l.ArtistId,
                Stage = l.Stage,
                StartTime = l.StartTime,
            }).Distinct().ToList();

        }
        catch (Exception e)
        {
            throw new Exception($"Error while trying to return lineups for festival with id {festivalId}: {e.Message}");
        }
    }

    public async Task<List<ArtistGetDto>> GetArtistsForFestivalAsync(int festivalId)
    {
        try
        {
            var festival = await festivalRepository.GetByIdAsync(festivalId);

            if (festival == null)
            {
                throw new Exception($"Festival with id {festivalId} not found while trying to get festival artists");
            }
            
            return festival.Artists.Select(a => new ArtistGetDto()
            {
                Id = a.Id,
                Name = a.Name,
                Genre = a.Genre,
                Image = a.Image,
            }).Distinct().ToList();
        }
        catch (Exception e)
        {
            throw new Exception($"Error while trying to retire artists for festival with id {festivalId}: {e.Message}");
        }
    }
    
    public async Task AddFestivalLineupAsync(int festivalId, LineupGetDto lineupDto)
    {
        try
        {
            var lineup = new Lineup()
            {
                FestivalId = festivalId,
                ArtistId = lineupDto.ArtistId,
                Stage = lineupDto.Stage,
                StartTime = lineupDto.StartTime,
            };
            await festivalRepository.AddFestivalLineupAsync(festivalId, lineup);
        }
        catch (Exception e)
        {
            throw new Exception($"Error while trying to add line up to the festival with id: {festivalId}: {e.Message}");
        }
    }
}