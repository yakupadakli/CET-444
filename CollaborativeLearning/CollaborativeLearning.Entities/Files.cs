﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeLearning.Entities
{
    class Files:Base
    {

        [Required(ErrorMessage = "Dosya Adı alanı boş bırakılamaz")]
        [Display(Name = "Dosya Adı")]
        [MaxLength(50, ErrorMessage = "{0} karakterden uzun olamaz")]
        public string fileName { get; set; }

        public int FileSize { get; set; }
        public string FileType { get; set; }

    }
}