using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class Group
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

       
        [Required(ErrorMessage = "Grup Adı alanı boş bırakılamaz")]
        [Display(Name = "Grup Adı")]
        [MaxLength(50, ErrorMessage = "{0} karakterden uzun olamaz")]
        public string GroupName { get; set; }

        public int SemesterID { get; set; }
        public virtual Semester Semester { get; set; }

        [Required(ErrorMessage = "Kayıt Tarihi alanı boş bırakılamaz")]
        [Display(Name = "Kayıt Tarihi")]
        public int RegUserID { get; set; }
        [Required]
        public DateTime RegDate { get; set; }
     

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Scenario> Scenarios { get; set; }
        public virtual ICollection<GroupWork> GroupWorks { get; set; }
        public virtual ICollection<MeetingNote> MeetingNotes { get; set; }
    }
}
