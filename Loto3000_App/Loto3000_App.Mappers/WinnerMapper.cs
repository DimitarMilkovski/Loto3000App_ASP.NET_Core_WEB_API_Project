using Loto3000_App.Domain;
using Loto3000_App.DTOs.WinnerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000_App.Mappers
{
    public static class WinnerMapper
    {
        public static WinnerDto ToWinnerDto (this Winner winner)
        {
            return new WinnerDto
            {
                UserFullName = $"{winner.User.Firstname} {winner.User.Lastname}",
                Prize = winner.Prize ,
                SessionStartDate = winner.Session.StartDate,
                SessionEndDate = winner.Session.EndDate,
                TicketNumber = winner.TicketNumber,
                WinningCombination = winner.WinningCombination.ToCombinationDto(),
                
            };

        }
    }
}
