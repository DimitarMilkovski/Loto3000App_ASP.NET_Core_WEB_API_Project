using Loto3000_App.Domain;
using Loto3000_App.DTOs.TicketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000_App.Mappers
{
    public static class TicketMapper
    {
        public static TicketDto ToTicketDto (this Ticket ticket)
        {
            return new TicketDto
            {
                TicketNumber = ticket.TicketNumber,
                UserFullname = $"{ticket.User.Firstname} {ticket.User.Lastname}",
                SessionStartDate = ticket.Session.StartDate,
                SessionEndDate = ticket.Session.EndDate,
                Combinations = ticket.Combinations.Select(x=>x.ToCombinationDto()).ToList()
            };
        }

        public static CombinationDto ToCombinationDto(this Combination combination)
        {
            return new CombinationDto
            {
                Number1 = combination.Number1,
                Number2 = combination.Number2,
                Number3 = combination.Number3,
                Number4 = combination.Number4,
                Number5 = combination.Number5,
                Number6 = combination.Number6,
                Number7 = combination.Number7

            };
        }
        public static Combination ToCombination (this CombinationDto combinationDto, Ticket ticket)
        {
            return new Combination
            {
                Ticket = ticket,
                TicketId = ticket.Id,
                Number1 = combinationDto.Number1,
                Number2 = combinationDto.Number2,
                Number3 = combinationDto.Number3,
                Number4 = combinationDto.Number4,
                Number5 = combinationDto.Number5,
                Number6 = combinationDto.Number6,
                Number7 = combinationDto.Number7,
            };
                    

        }


    }
}
