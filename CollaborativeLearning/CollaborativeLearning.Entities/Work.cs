using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class Work
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required(ErrorMessage = "This field cannot be empty!")]
        [Display(Name = "Name")]
        [MaxLength(50, ErrorMessage = "Length must be {0} character(s) ")]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }

        public int OrderID { get; set; }
        [Required]
        public DateTime RegDate { get; set; }

        [Display(Name = "Is Active")]
        public bool isActive { get; set; }

        [Required]
        public int RegUserID { get; set; }

        public virtual ICollection<Scenario> Scenarios { get; set; }
        public virtual ICollection<GroupWork> GroupWorks { get; set; }

    }
}
