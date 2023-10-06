using Loto3000_App.Domain.Enums;
using Loto3000_App.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loto3000_App.DTOs.TicketDtos;

namespace Loto3000_App.DTOs.WinnerDtos
{
    public class WinnerDto
    {
        public string UserFullName { get; set; }
        public PrizeEnum Prize { get; set; }
        public DateTime SessionStartDate { get; set; }
        public DateTime SessionEndDate { get; set; }
        public int TicketNumber { get; set; }
        public CombinationDto WinningCombination { get; set; }

    }
}
