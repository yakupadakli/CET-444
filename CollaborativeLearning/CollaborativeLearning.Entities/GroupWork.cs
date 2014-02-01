using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class GroupWork:BaseEntity
    {
        public int workID { get; set; }
        public virtual Work Works { get; set; }

        public int groupID { get; set; }
        public virtual Group Groups { get; set; }


        public int statusID { get; set; }
        public virtual Status Status { get; set; }
    }
}
