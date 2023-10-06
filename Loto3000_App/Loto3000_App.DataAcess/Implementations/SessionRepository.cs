using Loto3000_App.DataAcess.Interfaces;
using Loto3000_App.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000_App.DataAcess.Implementations
{
    public class SessionRepository : ISessionRepository
    {
        public Loto3000DbContext _dbContext;
        public SessionRepository( Loto3000DbContext context)
        {
            _dbContext = context;
        }
        public void Add(Session entity)
        {
            _dbContext.Sessions.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(Session entity)
        {
            _dbContext.Remove(entity);
            _dbContext.SaveChanges();
        }

        public List<Session> GetAll()
        {
            return _dbContext.Sessions.ToList();
        }

        public Session GetById(int id)
        {
            return _dbContext.Sessions.FirstOrDefault(x => x.Id == id);
        }

        public Session GetOngoingSession()
        {
            return _dbContext.Sessions.FirstOrDefault(x => x.Ongoing == true);
        }

        public void Update(Session entity)
        {
            _dbContext.Sessions.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
