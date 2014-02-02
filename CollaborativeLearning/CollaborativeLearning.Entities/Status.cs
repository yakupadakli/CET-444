using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class Status
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "Status")]
        public virtual string Value { get; set; }

        public virtual ICollection<Work> Works { get; set; }
        public virtual ICollection<Semester> Semesters { get; set; }
        public virtual ICollection<GroupWork> GroupWorks { get; set; }
        public virtual ICollection<ActionPlanList> ActionPlanLists { get; set; }
    }
}
