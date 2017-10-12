using Assemblify.Data.Models;
using Assemblify.Data.Repositories;
using Assemblify.Data.SaveContext;
using Assemblify.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assemblify.Infrastructure.Mapping;

namespace Assemblify.Services
{
    public class UsersService : IUsersService
    {
        private readonly IEfRepository<User> usersRepo;
        private readonly ISaveContext context;

        public UsersService(IEfRepository<User> usersRepo,
            ISaveContext context)
        {
            this.usersRepo = usersRepo;
            this.context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return this.usersRepo
                .All
                .ToList();
        }
    }
}
