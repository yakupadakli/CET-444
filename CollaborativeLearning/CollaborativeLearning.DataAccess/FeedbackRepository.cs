using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CollaborativeLearning.Entities;

namespace CollaborativeLearning.DataAccess
{
    public class FeedbackRepository : GenericRepository<Feedback>
    {
        public FeedbackRepository(DataContext context)
            : base(context)
        {
        }
    }
}
