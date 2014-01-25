using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeLearning.Entities
{
    class Work:Base
    {

        [Required(ErrorMessage = "İş Adı alanı boş bırakılamaz")]
        [Display(Name = "İş Adı")]
        [MaxLength(50, ErrorMessage = "{0} karakterden uzun olamaz")]
        public string groupName { get; set; }

        public int scenarioID { get; set; }
        public virtual Scenario Scenarios { get; set; }

        public int statusID { get; set; }
        public virtual Status Status { get; set; }


    }
}
