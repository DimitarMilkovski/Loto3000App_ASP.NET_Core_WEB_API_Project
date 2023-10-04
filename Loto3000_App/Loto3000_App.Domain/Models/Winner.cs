using Loto3000_App.Domain.Enums;
using System;
using System.Collections.Generic;
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
    }
}
