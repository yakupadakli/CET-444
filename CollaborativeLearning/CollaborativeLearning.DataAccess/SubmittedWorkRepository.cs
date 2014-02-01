using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CollaborativeLearning.Entities;

namespace CollaborativeLearning.DataAccess
{
    public class SubmittedWorkRepository : GenericRepository<SubmittedWork>
    {
        public SubmittedWorkRepository(DataContext context)
            : base(context)
        {
        }
    }
}
