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
            var status = new List<Status>
                {
                    new Status {value = "Read", regDate=DateTime.Now,regUserID = 1},
                    new Status {value = "Unread", regDate=DateTime.Now,regUserID = 1}
                };

            status.ForEach(p => context.Statuses.Add(p));

            
            context.SaveChanges();


        }
    }
}
