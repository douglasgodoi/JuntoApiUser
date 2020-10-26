using System.Collections.Generic;
using JuntoApi.Models;

namespace JuntoApi.Repositories
{
    public interface IUserRepository
    {
        public IEnumerable<User> Get();

        public User Find(string name, string pass);

        public User Save(User user);

        public User Update(User user);

        public User Delete(User user);
    }
}