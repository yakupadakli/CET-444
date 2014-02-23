using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class FeedbackSeenLog
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserID { get; set; }
        public virtual User User { get; set; }
        public int GroupWorkkID { get; set; }
        public virtual GroupWork GroupWork { get; set; }

        public Boolean isSeen { get; set; }

        [Display(Name = "Last Seen Date")]
        public DateTime lastSeenDate { get; set; }

        [Display(Name = "Date")]
        public DateTime regDate { get; set; }

        [Required]
        public int regUserID { get; set; }
    }
}
