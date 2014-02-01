using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class Status:BaseEntity
    {
        [Required(ErrorMessage = "Durum alanı boş bırakılamaz")]
        [Display(Name = "Durum")]
        [MaxLength(50, ErrorMessage = "{0} karakterden uzun olamaz")]
        public string value { get; set; }
    }
}
