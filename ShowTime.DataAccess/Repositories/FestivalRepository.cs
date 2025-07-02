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
}