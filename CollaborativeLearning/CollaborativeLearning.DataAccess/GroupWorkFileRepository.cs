using CollaborativeLearning.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeLearning.DataAccess
{
    public class GroupWorkFileRepository: GenericRepository<GroupWorkFile>
    {
        public GroupWorkFileRepository(DataContext context)
            : base(context)
        {
        }
    }
    
}
