﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeLearning.Entities
{
    public class GroupWorkFile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int GroupWorkID { get; set; }
        public virtual GroupWork GroupWork { get; set; }

        [Required(ErrorMessage = "Dosya Adı alanı boş bırakılamaz")]
        [Display(Name = "Dosya Adı")]
        [MaxLength(50, ErrorMessage = "{0} karakterden uzun olamaz")]
        public string FileName { get; set; }

        public int FileSize { get; set; }

        public string FileType { get; set; }

        public string FileUrl { get; set; }

        [Required(ErrorMessage = "Kayıt Tarihi alanı boş bırakılamaz")]
        [Display(Name = "Kayıt Tarihi")]
        public DateTime regDate { get; set; }

        [Required]
        public int regUserID { get; set; }
    }
}
