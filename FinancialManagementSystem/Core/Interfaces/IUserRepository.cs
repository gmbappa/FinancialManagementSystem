using Core.Entities;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetUsers(string name, string password);
       
    }
}
