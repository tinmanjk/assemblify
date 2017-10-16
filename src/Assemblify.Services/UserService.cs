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
    public class UserService : IUserService
    {
        private readonly IEfRepository<User> usersRepo;
        private readonly ISaveContext context;

        public UserService(IEfRepository<User> usersRepo,
            ISaveContext context)
        {
            this.usersRepo = usersRepo;
            this.context = context;
        }

        public User GetUserById(string id)
        {
            return this.usersRepo.GetById(id);
        }

        public IEnumerable<User> GetAll()
        {
            return this.usersRepo
                .All
                .ToList();
        }

        public IEnumerable<string> GetAllUserNames()
        {
            return this.usersRepo
                .All
                .Select(x=>x.UserName)
                .ToList();
        }
    }
}
