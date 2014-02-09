using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CollaborativeLearning.Entities;

namespace CollaborativeLearning.WebUI.Models
{
    public class SemesterModels:Semester
    {
        public int StudentCount { get; set; }
        public int MentorCount { get; set; }
    }

    
}