using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CollaborativeLearning.Entities;

namespace CollaborativeLearning.DataAccess
{
    public class TaskRepository : GenericRepository<Task>
    {
        public TaskRepository(DataContext context)
            : base(context)
        {
        }
    }
}
