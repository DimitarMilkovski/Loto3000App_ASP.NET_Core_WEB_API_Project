using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000_App.Domain
{
    public class Session : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Ongoing { get; set; }
        public List<Ticket> TicketList { get; set; }
        public List<Winner> WinnerList { get; set; }

        
    }
}
