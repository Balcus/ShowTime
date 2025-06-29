using ShowTime.DataAccess.Models;

namespace ShowTime.DataAccess.Repositories.Interfaces;

public interface IFestivalRepository : IRepository<Festival>
{
    Task<ICollection<Artist>?> GetFestivalArtists(int id);
    Task<ICollection<Lineup>?> GetFestivalLineups(int id);
}