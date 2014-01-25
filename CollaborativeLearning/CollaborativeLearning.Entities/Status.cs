using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeLearning.Entities
{
    class Status : Base
    {
       


        [Required(ErrorMessage = "Durum alanı boş bırakılamaz")]
        [Display(Name = "Durum")]
        [MaxLength(50, ErrorMessage = "{0} karakterden uzun olamaz")]
        public string value { get; set; }

    }
}
