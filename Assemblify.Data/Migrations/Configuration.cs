namespace Assemblify.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<MsSqlDbContext>
    {
        private const string AdministratorUserName = "admin@admin.com";
        private const string AdministratorPassword = "asdasd";
        private const string AdministratorRoleName = "Admin";


        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(MsSqlDbContext context)
        {
            this.SeedRoles(context);
            this.SeedUsers(context);
            this.SeedSamplePosts(context);

            base.Seed(context);
        }

        private void SeedRoles(MsSqlDbContext context)
        {
            if (!context.Roles.Any())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var roleNames = new List<string>
            {
                AdministratorRoleName,
                "User"
            };

                foreach (var roleName in roleNames)
                {
                    var role = new IdentityRole { Name = roleName };
                    roleManager.Create(role);
                }
            }
        }

        private void SeedUsers(MsSqlDbContext context)
        {
            if (!context.Users.Any())
            {
                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var user = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = AdministratorUserName,
                    Email = AdministratorUserName,
                    EmailConfirmed = true,
                };

                userManager.Create(user, AdministratorPassword);
                userManager.AddToRole(user.Id, AdministratorRoleName);
            }
        }

        private void SeedSamplePosts(MsSqlDbContext context)
        {
            if (!context.Posts.Any())
            {
                for (int i = 0; i < 5; i++)
                {
                    var post = new Post()
                    {
                        Title = "Post " + i,
                        Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed sit amet lobortis nibh. Nullam bibendum, tortor quis porttitor fringilla, eros risus consequat orci, at scelerisque mauris dolor sit amet nulla. Vivamus turpis lorem, pellentesque eget enim ut, semper faucibus tortor. Aenean malesuada laoreet lorem.",
                        Author = context.Users.First(x => x.Email == AdministratorUserName),
                        CreatedOn = DateTime.UtcNow
                    };

                    context.Posts.Add(post);
                }
            }
        }
    }
}
