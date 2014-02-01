using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class Group
    {
        [Required(ErrorMessage = "Grup Adı alanı boş bırakılamaz")]
        [Display(Name = "Grup Adı")]
        [MaxLength(50, ErrorMessage = "{0} karakterden uzun olamaz")]
        public string groupName { get; set; }

        public int semesterID { get; set; }
        public virtual Semester Semester { get; set; }

        [Required(ErrorMessage = "Kayıt Tarihi alanı boş bırakılamaz")]
        [Display(Name = "Kayıt Tarihi")]
        public DateTime regDate { get; set; }
        public int statusID { get; set; }
        public virtual Status Status { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
