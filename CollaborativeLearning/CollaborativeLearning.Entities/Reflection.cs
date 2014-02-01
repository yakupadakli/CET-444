using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class Reflection
    {
        public int semesterID { get; set; }
        public virtual Semester Semester { get; set; }

        public int userID { get; set; }

        public virtual User user { get; set; }
        [Required(ErrorMessage = "Başlık alanı boş bırakılamaz")]
        [Display(Name = " Başlık")]
        [MaxLength(50, ErrorMessage = "{0} karakterden uzun olamaz")]
        public string title { get; set; }


        [Required(ErrorMessage = "İçerik alanı boş bırakılamaz")]
        [Display(Name = " İçerik")]
        [MaxLength(500, ErrorMessage = "{0} karakterden uzun olamaz")]
        public string content { get; set; }

        public int statusID { get; set; }
        public virtual Status Status { get; set; }

        public Boolean isRead { get; set; }
    }
}
