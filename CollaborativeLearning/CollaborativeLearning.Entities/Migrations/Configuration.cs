
namespace CollaborativeLearning.Entities.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    
    internal sealed class Configuration : DbMigrationsConfiguration<CollaborativeLearning.Entities.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CollaborativeLearning.Entities.DataContext context)
        {

            context.Users.AddOrUpdate
                (
                new CollaborativeLearning.Entities.User {
                    Username = "Admin",
                    FirstName = "Hamdi",
                    LastName = "Erkunt",
                    Email = "erkunt@boun.edu.tr",
                    PhoneNumber = "05009990909",
                    Gender = "Male",
                    IsApproved = true,
                    IsLockedOut = false,
                    LastLockoutDate = DateTime.UtcNow,
                    LastActivityDate = DateTime.UtcNow,
                    LastPasswordChangedDate = DateTime.UtcNow,
                    LastPasswordFailureDate = DateTime.UtcNow,
                    Password = CollaborativeLearning.WebUI.Membership.Crypto.HashPassword("123456"),
                    CreateDate = DateTime.UtcNow,
                    PasswordFailuresSinceLastSuccess = 0,
                    RoleID = 1
                }
                );
            context.Users.AddOrUpdate
                (
                new CollaborativeLearning.Entities.User
                {
                    Username = "SuperAdmin",
                    FirstName = "Super",
                    LastName = "Admin",
                    Email = "cetyool@gmail.com",
                    PhoneNumber = "05009990909",
                    Gender = "Male",
                    IsApproved = true,
                    IsLockedOut = false,
                    LastLockoutDate = DateTime.UtcNow,
                    LastActivityDate = DateTime.UtcNow,
                    LastPasswordChangedDate = DateTime.UtcNow,
                    LastPasswordFailureDate = DateTime.UtcNow,
                    Password = CollaborativeLearning.WebUI.Membership.Crypto.HashPassword("123asd"),
                    CreateDate = DateTime.UtcNow,
                    PasswordFailuresSinceLastSuccess = 0,
                    RoleID = 1
                }
                );
            context.Roles.AddOrUpdate(
                new CollaborativeLearning.Entities.Role { RoleName = "Instructor", Description = "Teacher" },
                new CollaborativeLearning.Entities.Role { RoleName = "Mentor", Description = "Assistant" },
                new CollaborativeLearning.Entities.Role { RoleName = "Student", Description = "Default User Type" }

            );

             

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
