﻿using CollaborativeLearning.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeLearning.DataAccess
{
    public class MeetingNoteRepository: GenericRepository<MeetingNote>
    {
        public MeetingNoteRepository(DataContext context)
            : base(context)
        {
        }

    }
}
