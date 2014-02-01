using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CollaborativeLearning.Entities;

namespace CollaborativeLearning.DataAccess
{
    public class ResourceFileRepository : GenericRepository<ResourceFile>
    {
        public ResourceFileRepository(DataContext context)
            : base(context)
        {
        }
    }
}
