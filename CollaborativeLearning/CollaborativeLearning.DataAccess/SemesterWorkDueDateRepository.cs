using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CollaborativeLearning.Entities;

namespace CollaborativeLearning.DataAccess
{
    public class SemesterWorkDueDateRepository : GenericRepository<SemesterWorkDueDate>
    {
        public SemesterWorkDueDateRepository(DataContext context)
            : base(context)
        {
        }
    }
}
