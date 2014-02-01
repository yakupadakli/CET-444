using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CollaborativeLearning.Entities;

namespace CollaborativeLearning.DataAccess
{
    public class WorkRepository : GenericRepository<Work>
    {
        public WorkRepository(DataContext context)
            : base(context)
        {
        }
    }
}
