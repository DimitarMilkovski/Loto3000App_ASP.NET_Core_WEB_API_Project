using Loto3000_App.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000_App.DTOs.TicketDtos
{
    public class TicketDto
    {
        public int TicketNumber { get; set; }
        public string UserFullname { get; set; }
        public DateTime SessionStartDate { get; set; }
        public DateTime SessionEndDate { get; set; }
        public List<CombinationDto> Combinations { get; set; }
    }
}
