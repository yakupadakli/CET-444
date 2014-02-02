using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CollaborativeLearning.Entities;
using System.Data.Entity;

namespace CollaborativeLearning.DataAccess
{
    public class RoleRepository : GenericRepository<Role>
    {
        public RoleRepository(DataContext context)
            : base(context)
        {
        }
    }
}
