using Assemblify.Data.Models;
using Assemblify.Infrastructure.Mapping;
using System.Collections.Generic;
using System.Linq;

namespace Assemblify.Services.Contracts
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        IEnumerable<string> GetAllUserNames();
        User GetUserById(string id);
    }
}