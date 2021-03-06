﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class Reflection
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int semesterID { get; set; }
        public virtual Semester Semester { get; set; }

        public int userID { get; set; }
        public virtual User user { get; set; }
        
        [Required(ErrorMessage = "Başlık alanı boş bırakılamaz")]
        [MaxLength(50, ErrorMessage = "{0} karakterden uzun olamaz")]
        public string Title { get; set; }


        [Required(ErrorMessage = "İçerik alanı boş bırakılamaz")]
        [MaxLength(500, ErrorMessage = "{0} karakterden uzun olamaz")]
        public string Content { get; set; }

       
        public Boolean isRead { get; set; }
        [Required(ErrorMessage = "Kayıt Tarihi alanı boş bırakılamaz")]
        [Display(Name = "Kayıt Tarihi")]
        public DateTime regDate { get; set; }

        [Required]
        public int regUserID { get; set; }
    }
}
