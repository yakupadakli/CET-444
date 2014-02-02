using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class DataContextInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            //var roles = new List<Role>
            //{
            //    new Role{RoleName="Instructor", Description="Teacher"},
                    
            //    new Role{RoleName="Mentor", Description="Assistant"},
                
            //    new Role{RoleName="Student", Description="Default User Type"}
            //};

            //roles.ForEach(r => context.Roles.Add(r));

            //var status = new List<Status>
            //{
            //    new Status{Value="Read"},
            //    new Status{Value="Unread"}
            //};

            //status.ForEach(r => context.Statuses.Add(r));

            //context.SaveChanges();


        }
    }
}
