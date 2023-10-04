using Loto3000_App.DataAcess.Interfaces;
using Loto3000_App.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000_App.DataAcess.Implementations
{
    public class CombinationRepository : IRepository<Combination>
    {
        public Loto3000DbContext _dbContext;
        public CombinationRepository(Loto3000DbContext context)
        {
            _dbContext = context;
        }
        public void Add(Combination entity)
        {
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(Combination entity)
        {
            throw new NotImplementedException();
        }

        public List<Combination> GetAll()
        {
            throw new NotImplementedException();
        }

        public Combination GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Combination entity)
        {
            throw new NotImplementedException();
        }
    }
}
