using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CollaborativeLearning.Entities;

namespace CollaborativeLearning.DataAccess
{
    public class FileRepository : GenericRepository<File>
    {
        public FileRepository(DataContext context)
            : base(context)
        {
        }
    }
}
