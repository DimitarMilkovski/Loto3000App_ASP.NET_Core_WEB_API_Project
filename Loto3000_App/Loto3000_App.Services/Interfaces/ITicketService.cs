using Loto3000_App.Domain;
using Loto3000_App.DTOs.TicketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000_App.Services.Interfaces
{
    public interface ITicketService
    {
        List<TicketDto> GetAllTickets();
        List<TicketDto> GetPlayersTickets(int userId);
        List<TicketDto> GetOngoingSesssionTickets(int userId);
        TicketDto GetById(int id);

        void CreateTicket(List<CombinationDto> combinations, int userId);
        void DeleteTicket(int id, int userId);
    }
}
