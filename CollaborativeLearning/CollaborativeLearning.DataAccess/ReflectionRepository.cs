using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CollaborativeLearning.Entities;

namespace CollaborativeLearning.DataAccess
{
    public class ReflectionRepository: GenericRepository<Reflection>
    {
        public ReflectionRepository(DataContext context)
            : base(context)
        {
        }
    }
}
