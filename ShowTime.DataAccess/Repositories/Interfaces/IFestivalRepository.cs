using ShowTime.DataAccess.Models;

namespace ShowTime.DataAccess.Repositories.Interfaces;

public interface IFestivalRepository : IRepository<Festival>
{
    Task<ICollection<Artist>?> GetFestivalArtists(int id);
    Task<ICollection<Lineup>?> GetFestivalLineups(int id);
    Task UpdateFestivalArtists(int id, List<Artist> artists);
}