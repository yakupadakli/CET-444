using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CollaborativeLearning.Entities;

namespace CollaborativeLearning.DataAccess
{
    public class ScenarioRepository : GenericRepository<Scenario>
    {
        public ScenarioRepository(DataContext context)
            : base(context)
        {
        }
    }
}
