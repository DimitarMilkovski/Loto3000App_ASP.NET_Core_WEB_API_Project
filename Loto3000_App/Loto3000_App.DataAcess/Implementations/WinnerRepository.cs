using Loto3000_App.DataAcess.Interfaces;
using Loto3000_App.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000_App.DataAcess.Implementations
{
    public class WinnerRepository : IWinnerRepository
    {
        private readonly Loto3000DbContext _dbContext;
        public WinnerRepository(Loto3000DbContext loto3000DbContext)
        {
            _dbContext = loto3000DbContext;
        }
        public void Add(Winner entity)
        {
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(Winner entity)
        {
            throw new NotImplementedException();
        }

        public List<Winner> GetAll()
        {
            return _dbContext.Winners.ToList();
        }

        public Winner GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Winner> GetBySession(int sessionId)
        {
            return _dbContext.Winners.Where(x=>x.SessionId == sessionId).ToList();
        }

        public void Update(Winner entity)
        {
            throw new NotImplementedException();
        }
    }
}
