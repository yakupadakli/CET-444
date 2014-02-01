using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CollaborativeLearning.Entities;

namespace CollaborativeLearning.DataAccess
{
    public class SemesterRepository : GenericRepository<Semester>
    {
        public SemesterRepository(DataContext context)
            : base(context)
        {
        }
    }
}
