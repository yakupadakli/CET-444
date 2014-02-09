using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class GroupWork
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        

        public int WorkId { get; set; }
        public virtual Work Works { get; set; }

        public int GroupID { get; set; }
        public virtual Group Groups { get; set; }

        [Required(ErrorMessage = "This field cannot be empty!")]
        [Display(Name = "Submission Date")]
        public DateTime SubmissionDate { get; set; }

        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }

       
        [Required(ErrorMessage = "This field cannot be empty!")]
        public DateTime regDate { get; set; }

        [Required]
        public int regUserID { get; set; }
       
    }
}
