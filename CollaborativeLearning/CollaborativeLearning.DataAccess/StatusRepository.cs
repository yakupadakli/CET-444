using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CollaborativeLearning.Entities;

namespace CollaborativeLearning.DataAccess
{
    public class StatusRepository : GenericRepository<Status>
    {
        public StatusRepository(DataContext context)
            : base(context)
        {
        }
    }
}
