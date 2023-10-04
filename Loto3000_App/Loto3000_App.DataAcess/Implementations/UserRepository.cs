using Loto3000_App.DataAcess.Interfaces;
using Loto3000_App.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000_App.DataAcess.Implementations
{
    public class UserRepository : IUserRepository
    {
        private Loto3000DbContext _dbContext;

        public UserRepository(Loto3000DbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void Add(User entity)
        {
            _dbContext.Users.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(User entity)
        {
            _dbContext.Users.Remove(entity);
            _dbContext.SaveChanges();
            
        }

        public List<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }

        public User GetById(int id)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public User GetUserByUsername(string username)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower());
        }

        public User LoginUser(string username, string hashedPassword)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower()
            && x.Password == hashedPassword);
        }

        public void Update(User entity)
        {
            _dbContext.Update(entity);
            
        }
    }
}
