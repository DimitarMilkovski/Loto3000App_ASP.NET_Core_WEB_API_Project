﻿using Loto3000_App.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000_App.DataAcess.Interfaces
{
    public interface ISessionRepository: IRepository<Session>
    {
        Session GetOngoingSession();
        Session GetLastExpiredSession();
    }
}
