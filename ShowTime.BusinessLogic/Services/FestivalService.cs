using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.Dto.FestivalDto;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Interfaces;

namespace ShowTime.BusinessLogic.Services;

public class FestivalService(IRepository<Festival> festivalRepository) : IFestivalService
{
    public async Task<IList<FestivalGetDto>> GetAllAsync()
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

    public async Task<FestivalGetDto?> GetByIdAsync(int id)
    {
        try
        {
            var festival = await festivalRepository.GetByIdAsync(id);
            if (festival != null)
            {
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
            return null;
        }
        catch (Exception e)
        {
            throw new Exception($"An error occured while retrieving festival with id {id} : {e.Message}");
        }
    }

    public async Task AddAsync(FestivalCreateDto festivalCreateDto)
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

    public async Task UpdateAsync(int id, FestivalCreateDto festivalUpdateDro)
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

    public async Task DeleteAsync(int id)
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
}