using ShowTime.BusinessLogic.Dto.TicketDto;
using ShowTime.DataAccess.Models;

namespace ShowTime.BusinessLogic.Abstractions;

public interface ITicketService
{
    Task UpdateTicketAsync(int ticketId, TicketCreateDto editedTicket);
}