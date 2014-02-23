using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class Task
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "This field cannot be empty!")]
        [Display(Name = "Task Name")]
        public string TaskName { get; set; }

        [Required(ErrorMessage = "This field cannot be empty!")]
        [DataType(DataType.Html)]
        public string Content { get; set; }
        
        [Display(Name = "Task Name")]
        public bool isActive { get; set; }
        public int OrderID { get; set; }
        public int RegUserId { get; set; }
        public DateTime RegDateTime { get; set; }

        public virtual ICollection<Scenario> Scenarios { get; set; }

    }
}
