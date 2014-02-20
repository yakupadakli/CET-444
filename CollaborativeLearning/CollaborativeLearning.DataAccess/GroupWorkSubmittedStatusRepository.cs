using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CollaborativeLearning.Entities;

namespace CollaborativeLearning.DataAccess
{
    public class GroupWorkSubmittedStatusRepository : GenericRepository<GroupWorkSubmittedStatus>
    {
        public GroupWorkSubmittedStatusRepository(DataContext context)
            : base(context)
        {
        }
    }
}
