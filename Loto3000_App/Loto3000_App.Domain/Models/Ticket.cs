using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000_App.Domain
{
    public class Ticket : BaseEntity
    {
        public int TicketNumber { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Session Session { get; set; }
        public int SessionId { get; set; }
        public List<Combination> Combinations { get; set; }

    }
}
