using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CollaborativeLearning.Entities;

namespace CollaborativeLearning.DataAccess
{
    public class ResourceRepository : GenericRepository<Resource>
    {
        public ResourceRepository(DataContext context)
            : base(context)
        {
        }
    }
}
