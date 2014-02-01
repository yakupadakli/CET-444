using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CollaborativeLearning.Entities;

namespace CollaborativeLearning.DataAccess
{
    public class GroupRepository: GenericRepository<Group>
    {
        public GroupRepository(DataContext context)
            : base(context)
        {
        }
    }
}
