using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.Dto.TicketDto;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Interfaces;

namespace ShowTime.BusinessLogic.Services;

public class TicketService(IRepository<Ticket> ticketRepository) : ITicketService
{
    public async Task UpdateTicketAsync(int ticketId, TicketCreateDto editedTicket)
    {
        try
        {
            var ticket = await ticketRepository.GetByIdAsync(ticketId);
            if (ticket == null) return;
            ticket.Name = editedTicket.Name;
            ticket.Type = editedTicket.Type;
            ticket.Quantity = editedTicket.Quantity;
            ticket.Price = editedTicket.Price;
            await ticketRepository.UpdateAsync(ticket);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}