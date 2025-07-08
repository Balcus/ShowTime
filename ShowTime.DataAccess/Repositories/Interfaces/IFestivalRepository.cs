using ShowTime.DataAccess.Models;

namespace ShowTime.DataAccess.Repositories.Interfaces;

public interface IFestivalRepository : IRepository<Festival>
{
    Task<ICollection<Artist>?> GetFestivalArtistsAsync(int id);
    Task<ICollection<Lineup>?> GetFestivalLineupsAsync(int id);
    Task UpdateFestivalArtistsAsync(int id, List<Artist> artists);
    Task AddFestivalLineupAsync(int festivalId, Lineup lineup);
    Task AddTicketAsync(int festivalId, Ticket ticket);
    Task <List<Ticket>> GetFestivalTicketsAsync(int festivalId);
}