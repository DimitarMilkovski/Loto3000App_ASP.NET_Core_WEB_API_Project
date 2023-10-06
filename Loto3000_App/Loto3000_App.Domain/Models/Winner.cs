using Loto3000_App.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000_App.Domain
{
    public class Winner : BaseEntity
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public PrizeEnum Prize { get; set; }
        public Session Session { get; set; }
        public int SessionId { get; set; }
        public int TicketNumber { get; set; }
        public Combination? WinningCombination { get; set; }
        public int? WinningCombinationId { get; set; }



    }
}
