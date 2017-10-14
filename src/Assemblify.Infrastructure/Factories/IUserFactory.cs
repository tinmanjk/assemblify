using Assemblify.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Infrastructure.Factories
{
    public interface IUserFactory
    {
        User CreateUser(string username, string email);
    }
}
