using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CollaborativeLearning.Entities;
using CollaborativeLearning.DataAccess;
namespace CollaborativeLearning.WebUI.Models
{
    public class GroupScenarioWork
    {
        public int GroupID { get; set; }
        public virtual Group Group { get; set; }
        public int WorkSemesterDueDateID { get; set; }
        public virtual WorkSemesterDueDate WorkSemesterDueDate { get; set; }

        public int GroupWorkID { get; set; }
        public virtual GroupWork GroupWork { get; set; }
    }
}