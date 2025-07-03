using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Interfaces;

namespace ShowTime.DataAccess.Repositories;

public class FestivalRepository(ShowTimeDbContext context) : BaseRepository<Festival>(context), IFestivalRepository
{
    public override async Task<IEnumerable<Festival>> GetAllAsync()
    {
        return await Context
            .Set<Festival>()
            .Include(f => f.Lineups)
            .Include(f => f.Artists)
            .ToListAsync();
    }

    public override async Task<Festival?> GetByIdAsync(int id)
    {
        return await Context
            .Set<Festival>()
            .Include(f => f.Lineups)
            .Include(f => f.Artists)
            .FirstOrDefaultAsync(f => f.Id == id);
    }
    public async Task<ICollection<Artist>?> GetFestivalArtists(int id)
    {
        try
        {
            var festival = await Context
                .Set<Festival>()
                .Include(f => f.Artists)
                .FirstOrDefaultAsync(f => f.Id == id);

            return festival?.Artists;
        }
        catch (Exception e)
        {
            throw new Exception($"Error at trying to retrieve artists for festival with id {id}: {e.Message}");
        }
    }

    public async Task<ICollection<Lineup>?> GetFestivalLineups(int id)
    {
        try
        {
            var festival = await Context
                .Set<Festival>()
                .Include(f => f.Lineups)
                .FirstOrDefaultAsync(f => f.Id == id);

            return festival?.Lineups;
        }
        catch (Exception e)
        {
            throw new Exception($"Error at trying to retrieve lineups for festival with id {id}: {e.Message}");
        }
    }

    public async Task UpdateFestivalArtists(int id, List<Artist> artists)
    {
        var festival = await Context
            .Set<Festival>()
            .Include(f => f.Artists)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (festival == null) return;
        
        festival.Artists.Clear();

        foreach (var artist in artists)
        {
            var existingArtist = await Context
                .Set<Artist>()
                .FirstOrDefaultAsync(a => a.Id == artist.Id);

            if (existingArtist != null)
            {
                festival.Artists.Add(existingArtist);
            }
            else
            {
                throw new Exception($"Artist with ID {artist.Id} not found");
            }
        }
        await Context.SaveChangesAsync();
    }
}