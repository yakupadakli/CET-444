using CollaborativeLearning.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeLearning.DataAccess
{
    public class StudentCourseRequestRepository : GenericRepository<StudentCourseRequest>
    {
        public StudentCourseRequestRepository(DataContext context)
            : base(context)
        {
        }
    }
}
