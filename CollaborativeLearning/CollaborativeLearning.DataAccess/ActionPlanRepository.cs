using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CollaborativeLearning.Entities;

namespace CollaborativeLearning.DataAccess
{
    public class ActionPlanRepository : GenericRepository<ActionPlan>
    {
        public ActionPlanRepository(DataContext context)
            : base(context)
        {
        }
    }
}
