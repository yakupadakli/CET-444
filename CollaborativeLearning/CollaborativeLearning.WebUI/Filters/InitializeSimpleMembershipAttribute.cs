using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using WebMatrix.WebData;
using System.Web.Security;
using CollaborativeLearning.WebUI.Membership;
using CollaborativeLearning.Entities;
using CollaborativeLearning.DataAccess;

namespace CollaborativeLearning.WebUI.Filters
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<Entities.DataContext>(null);

                try
                {
                    using (var context = new Entities.DataContext())
                    {
                        if (!context.Database.Exists())
                        {
                            // Create the SimpleMembership database without Entity Framework migration schema
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();


                            UnitOfWork Db = new UnitOfWork();

                            var roles = new List<Role>
                            {
                                new Role{RoleName="Instructor", Description="Teacher"},
                    
                                new Role{RoleName="Mentor", Description="Assistant"},
                
                                new Role{RoleName="Student", Description="Default User Type"}
                            };

                            foreach (var role in roles)
                            {
                                Db.RoleRepository.Insert(role);
                            }
                            
                            var status = new List<Status>
                            {
                                new Status{Value="Read"},
                                new Status{Value="Unread"}
                            };

                            foreach (var item in status)
                            {
                                Db.StatusRepository.Insert(item);       
                            }


                            Db.Save();

                            User user = new User
                            {
                                Username = "Admin",
                                FirstName = "Hamdi",
                                LastName = "Erkunt",
                                Email = "erkunt@boun.edu.tr",
                                IsApproved = true,
                                IsLockedOut = false,
                                Password = Crypto.HashPassword("123456"),
                                CreateDate = DateTime.UtcNow,
                                PasswordFailuresSinceLastSuccess = 0
                            };


                            Db.UserRepository.Insert(user);

                            Db.Save();

                            Roles.AddUserToRole(user.Username, "Instructor");


                        }

                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }
        }
    }
}
