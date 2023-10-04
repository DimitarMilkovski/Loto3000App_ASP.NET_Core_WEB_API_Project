using Loto3000_App.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000_App.Domain
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public List<Ticket> Tickets { get; set; }
        public List<Winner> WonPrizes { get; set; }
        public RoleEnum Role { get; set; }


    }
}
