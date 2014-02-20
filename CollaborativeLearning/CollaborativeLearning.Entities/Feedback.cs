using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class Feedback
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserID { get; set;}
        public User User { get; set; }
        public int GroupWorkId { get; set; }
        public virtual GroupWork GroupWork { get; set; }

        [Required(ErrorMessage = "Dönüt alanı boş bırakılamaz")]
        [Display(Name = " Dönüt")]
        [MaxLength(500, ErrorMessage = "{0} karakterden uzun olamaz")]
        public string Content { get; set; }
                        
        public int parentID { get; set; }

        public virtual Feedback parent { get; set; }

        [Required(ErrorMessage = "Kayıt Tarihi alanı boş bırakılamaz")]
        [Display(Name = "Kayıt Tarihi")]
        public DateTime regDate { get; set; }
        [Required]
        public int regUserID { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
