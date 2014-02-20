using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CollaborativeLearning.Entities;

namespace CollaborativeLearning.DataAccess
{
    public class WorkSemesterDueDateRepository : GenericRepository<WorkSemesterDueDate>
    {
        public WorkSemesterDueDateRepository(DataContext context)
            : base(context)
        {
        }
    }
}
