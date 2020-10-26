using System.Collections.Generic;
using System.Linq;
using JuntoApi.Data;
using JuntoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JuntoApi.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly StoreDataContext _context;
        public UserRepository(StoreDataContext context)
        {
            _context = context;
        }

        public IEnumerable<User> Get()
        {
            return _context.Users.AsNoTracking().ToList();
        }

        public User Find(string name, string pass)
        {
            return _context.Users.AsNoTracking()
            .Where(x => x.UserName == name && x.Password == pass).FirstOrDefault();
        }

        public User Save(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User Update(User user)
        {
             _context.Entry<User>(user).State = EntityState.Modified;
            _context.SaveChanges();

            return user;
        }

        public User Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();

            return user;
        }
    }
}