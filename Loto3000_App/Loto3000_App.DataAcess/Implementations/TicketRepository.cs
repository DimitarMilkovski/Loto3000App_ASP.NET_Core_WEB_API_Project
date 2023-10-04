using Loto3000_App.DataAcess.Interfaces;
using Loto3000_App.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000_App.DataAcess.Implementations
{
    public class TicketRepository : ITicketRepository
    {
        private Loto3000DbContext _dbContext;

        public TicketRepository(Loto3000DbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void Add(Ticket entity)
        {
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(Ticket entity)
        {
            _dbContext.Remove(entity);
            _dbContext.SaveChanges();
        }

        public List<Ticket> GetAll()
        {
            return _dbContext.Tickets
                .Include(x => x.Session)
                .Include(x => x.Combinations)
                .Include(x => x.User)
                .ToList();
        }

        public Ticket GetById(int id)
        {
            return _dbContext.Tickets
                .Include(x => x.Session)
                .Include(x => x.Combinations)
                .Include(x => x.User)
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Ticket> GetByUserAndSessionId(int userId, int sessionId)
        {
            return _dbContext.Tickets
                .Include(x => x.Session)
                .Include(x => x.Combinations)
                .Include(x => x.User)
                .Where(x=>x.UserId == userId && x.SessionId == sessionId)
                .ToList();
        }

        public List<Ticket> GetByUserId(int userId)
        {
            return _dbContext.Tickets
                .Include(x => x.Session)
                .Include(x => x.Combinations)
                .Include(x => x.User)
                .Where(x => x.UserId == userId)
                .ToList();
        }

        public void Update(Ticket entity)
        {
            throw new NotImplementedException();
        }
    }
}
