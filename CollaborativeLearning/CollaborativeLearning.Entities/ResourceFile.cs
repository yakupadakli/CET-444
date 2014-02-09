﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class ResourceFile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

       
        public int ResourceID { get; set; }
        public virtual Resource Resource { get; set; }

        public int fileID { get; set; }
        public virtual File File { get; set; }

        [Required(ErrorMessage = "Kayıt Tarihi alanı boş bırakılamaz")]
        [Display(Name = "Kayıt Tarihi")]
        public DateTime regDate { get; set; }

        [Required]
        public int regUserID { get; set; }
    }
}
