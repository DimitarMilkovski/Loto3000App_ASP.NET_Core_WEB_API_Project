using Loto3000_App.DataAcess;
using Loto3000_App.DataAcess.Implementations;
using Loto3000_App.DataAcess.Interfaces;
using Loto3000_App.Domain;
using Loto3000_App.Services.Implementations;
using Loto3000_App.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000_App.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services)
        {
            services.AddDbContext<Loto3000DbContext>(x =>
            x.UseSqlServer("Server =.\\SQLExpress; Database = LotoAppDb1; Trusted_Connection = True; TrustServerCertificate = True"));
        }

        public static void InjectRepositories (IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITicketRepository, TicketRepository>();
            services.AddTransient<ISessionRepository, SessionRepository>();
            services.AddTransient<IRepository<Combination>, CombinationRepository>();
            services.AddTransient<IWinnerRepository, WinnerRepository>();
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<IDrawService, DrawService>();
        }
    }
}
