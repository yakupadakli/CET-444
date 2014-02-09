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

        [Required]
        public int regUserID { get; set; }
        [Required(ErrorMessage = "Grup Adı alanı boş bırakılamaz")]
        [Display(Name = "Grup Adı")]
        [MaxLength(50, ErrorMessage = "{0} karakterden uzun olamaz")]
        public string groupName { get; set; }

        public int semesterID { get; set; }
        public virtual Semester Semester { get; set; }

        [Required(ErrorMessage = "Kayıt Tarihi alanı boş bırakılamaz")]
        [Display(Name = "Kayıt Tarihi")]
        public DateTime regDate { get; set; }
     

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Semester> Semesters { get; set; }
        public virtual ICollection<Scenario> Scenarios { get; set; }
    }
}
