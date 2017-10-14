using Assemblify.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Infrastructure.Factories
{
    public interface IPostFactory
    {
        Post CreatePost(string title, string description, User user);

    }
}
