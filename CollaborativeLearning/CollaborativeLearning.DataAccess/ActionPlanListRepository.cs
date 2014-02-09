using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CollaborativeLearning.Entities;

namespace CollaborativeLearning.DataAccess
{
    public class ActionPlanListRepository : GenericRepository<ActionPlan>
    {
        public ActionPlanListRepository(DataContext context)
            : base(context)
        {
        }
    }
}
