using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class Resource
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required(ErrorMessage = "This field cannot be empty!")]
        [Display(Name = "Resource Name")]
        [MaxLength(50, ErrorMessage = "Length must be {0} character(s) ")]
        public string Name { get; set; }


        [Required(ErrorMessage = "This field cannot be empty!")]
        public string Description { get; set; }

       
        public bool isActive { get; set; }
        [Required(ErrorMessage = "This field cannot be empty!")]
        [Display(Name = "Resource Type")]
        [MaxLength(25, ErrorMessage = "Length must be {0} character(s) ")]
        public string type { get; set; }
        
        [Required]
        public DateTime RegDate { get; set; }

        [Required]
        public int RegUserID { get; set; }
        public virtual ICollection<Scenario> Scenarios { get; set; }
        public virtual ICollection<File> Files { get; set; }

    }
}
