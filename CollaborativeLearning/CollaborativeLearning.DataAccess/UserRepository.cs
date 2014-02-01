using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CollaborativeLearning.Entities;

namespace CollaborativeLearning.DataAccess
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(DataContext context)
            : base(context)
        {
        }
    }
}
