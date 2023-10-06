using Loto3000_App.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000_App.DTOs.TicketDtos
{
    public class UpdateTicketDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SessionId { get; set; }
        public List<CombinationDto> Combinations { get; set; }
    }
}
