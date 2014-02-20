using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeLearning.Entities
{
    public class MeetingNote
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int GroupID { get; set; }
        public virtual Group Group { get; set; }
        
        [Required(ErrorMessage = "This field cannot be empty!")]
        [Display(Name = "Name")]
        [MaxLength(50, ErrorMessage = "Length must be {0} character(s) ")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime regDate { get; set; }

        [Required]
        public int regUserID { get; set; }

        public virtual ICollection<MeetingNoteFile> MeetingNoteFiles {get; set; }
        
       
    }
}
