using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.Exceptions;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Interfaces;

namespace ShowTime.DataAccess.Repositories;

public class FestivalRepository(ShowTimeDbContext context) : BaseRepository<Festival>(context), IFestivalRepository
{
    public override async Task<IEnumerable<Festival>> GetAllAsync()
    {
        try
        {
            return await Context
                .Set<Festival>()
                .Include(f => f.Lineups)
                .Include(f => f.Artists)
                .Include(f => f.Tickets)
                .ToListAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"Error trying to get all festivals: {e.Message}");
        }
    }

    public override async Task<Festival?> GetByIdAsync(int id)
    {
        try
        {
            return await Context
                .Set<Festival>()
                .Include(f => f.Lineups)
                .Include(f => f.Artists)
                .FirstOrDefaultAsync(f => f.Id == id);
        }
        catch (Exception e)
        {
            throw new Exception($"Error trying to get festival with id {id}: {e.Message}");
        }
    }
    public async Task<ICollection<Artist>?> GetFestivalArtistsAsync(int id)
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

    public async Task<ICollection<Lineup>?> GetFestivalLineupsAsync(int id)
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

    public async Task UpdateFestivalArtistsAsync(int id, List<Artist> artists)
    {
        try
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
        catch (Exception e)
        {
            throw new Exception($"Error trying to update festival with id {id} artists: {e.Message}");
        }
    }

    public async Task AddFestivalLineupAsync(int festivalId, Lineup addedLineup)
    {
        try
        {
            var festival = await Context
                .Set<Festival>()
                .Include(f => f.Lineups)
                .FirstOrDefaultAsync(f => f.Id == festivalId);

            if (festival == null)
            {
                throw new EntityNotFoundException($"Festival with ID {festivalId} not found");
            }

            var lineup =
                festival.Lineups.FirstOrDefault(l => l.FestivalId == festivalId && l.ArtistId == addedLineup.ArtistId);
            if (lineup == null)
            {
                throw new EntityNotFoundException(
                    $"Lineup for festival {festivalId} with artist: {addedLineup.ArtistId} not found");
            }

            lineup.StartTime = addedLineup.StartTime;
            lineup.Stage = addedLineup.Stage;
            await Context.SaveChangesAsync();
        }
        catch (EntityNotFoundException e)
        {
            throw;
        }
        catch (Exception e)
        {
            throw new Exception($"Error while trying to add lineup to festival with id {festivalId}: {e.Message}");
        }
    }

    public async Task AddTicketAsync(int festivalId, Ticket ticket)
    {
        try
        {
            var festival = await Context
                .Set<Festival>()
                .Include(f => f.Tickets)
                .FirstOrDefaultAsync(f => f.Id == festivalId);

            if (festival == null)
            {
                throw new EntityNotFoundException($"Festival with ID {festivalId} not found");
            }

            festival.Tickets.Add(ticket);
            await Context.SaveChangesAsync();
        }
        catch (EntityNotFoundException e)
        {
            throw;
        }
        catch (Exception e)
        {
            throw new Exception($"Error trying to add ticket to festival with id {festivalId}: {e.Message}");
        }
    }

    public async Task BookTicketAsync(int festivalId, int ticketId)
    {
        try
        {
            var festival = await Context
                .Set<Festival>()
                .Include(f => f.Tickets)
                .FirstOrDefaultAsync(f => f.Id == festivalId);

            if (festival == null)
            {
                throw new EntityNotFoundException($"Festival with ID {festivalId} not found");
            }

            var ticket = festival.Tickets.FirstOrDefault(t => t.Id == ticketId);
            if (ticket == null)
            {
                throw new EntityNotFoundException($"Ticket with ID {ticketId} not found");
            }

            ticket.Quantity -= 1;
            await Context.SaveChangesAsync();
        }
        catch (EntityNotFoundException e)
        {
            throw;
        }
        catch (Exception e)
        {
            throw new Exception($"Error trying to make a booking for festival with id {festivalId} and ticket with id {ticketId}: {e.Message}");
        }
    }

    public async Task<List<Ticket>> GetFestivalTicketsAsync(int id)
    {
        try
        {
            var festival = await Context
                .Set<Festival>()
                .Include(f => f.Tickets)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (festival == null)
            {
                throw new EntityNotFoundException($"Festival with ID {id} not found");
            }

            return festival.Tickets.ToList();
        }
        catch (EntityNotFoundException e)
        {
            throw;
        }
        catch (Exception e)
        {
            throw new Exception($"Error trying to get tickets for festival with id {id}: {e.Message}");
        }
    }
}