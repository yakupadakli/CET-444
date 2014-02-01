using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CollaborativeLearning.Entities;

namespace CollaborativeLearning.DataAccess
{
    public class ScenarioTaskRepository : GenericRepository<ScenarioTask>
    {
        public ScenarioTaskRepository(DataContext context)
            : base(context)
        {
        }
    }
}
