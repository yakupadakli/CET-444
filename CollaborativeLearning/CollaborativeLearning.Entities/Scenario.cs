using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class Scenario
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Scenario name cannot be empty!")]
        [Display(Name = "Scenario Name")]
        [MaxLength(50, ErrorMessage = "Length must be {0} character(s) ")]
        public string Name { get; set; }
                     
        [Display(Name = "Short Description")]
        [MaxLength(140, ErrorMessage = "Length must be {0} character(s) ")]
        public string ShortDescription { get; set; }

        public bool isActive { get; set; }


        [Required]
        public DateTime RegDate { get; set; }
        [Required]
        public int RegUserID { get; set; }

        public virtual ICollection<Semester> Semesters { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<ActionPlan> ActionPlans { get; set; }
        public virtual ICollection<Work> Works { get; set; }
        public virtual ICollection<Resource> Resources { get; set; }
    }
}
