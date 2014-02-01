using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class ResourceList
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Kayıt Tarihi alanı boş bırakılamaz")]
        [Display(Name = "Kayıt Tarihi")]
        public DateTime regDate { get; set; }

        [Required]
        public int regUserID { get; set; }

        [Required(ErrorMessage = "Kaynak Adı alanı boş bırakılamaz")]
        [Display(Name = "Kaynak Adı")]
        [MaxLength(50, ErrorMessage = "{0} karakterden uzun olamaz")]
        public string name { get; set; }


        [Required(ErrorMessage = "Kaynak Tanımı alanı boş bırakılamaz")]
        [Display(Name = "Kaynak Tanımı")]
        [MaxLength(500, ErrorMessage = "{0} karakterden uzun olamaz")]
        public string description { get; set; }


        [Required(ErrorMessage = "Link alanı boş bırakılamaz")]
        [Display(Name = "Link")]
        [MaxLength(250, ErrorMessage = "{0} karakterden uzun olamaz")]
        public string link { get; set; }


        public int statusID { get; set; }
        public virtual Status Status { get; set; }

        [Required(ErrorMessage = "Kaynak Tipi alanı boş bırakılamaz")]
        [Display(Name = "Kaynak Tipi")]
        [MaxLength(25, ErrorMessage = "{0} karakterden uzun olamaz")]
        public string type { get; set; }
    }
}
