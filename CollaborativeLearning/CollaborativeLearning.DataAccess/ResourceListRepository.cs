﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CollaborativeLearning.Entities;

namespace CollaborativeLearning.DataAccess
{
    public class ResourceListRepository : GenericRepository<Resource>
    {
        public ResourceListRepository(DataContext context)
            : base(context)
        {
        }
    }
}
