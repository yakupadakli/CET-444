using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CollaborativeLearning.Entities;

namespace CollaborativeLearning.DataAccess
{
    public class SubmittedWorkStatusRepository : GenericRepository<SubmittedWorkStatus>
    {
        public SubmittedWorkStatusRepository(DataContext context)
            : base(context)
        {
        }
    }
}
