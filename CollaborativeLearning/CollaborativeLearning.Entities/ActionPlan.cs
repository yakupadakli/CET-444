﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CollaborativeLearning.Entities
{
    public class ActionPlan
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

       
        [Required(ErrorMessage = "Tanım alanı boş bırakılamaz")]
        [Display(Name = "Tanım")]
        [MaxLength(500, ErrorMessage = "{0} karakterden uzun olamaz")]
        public string Content { get; set; }

        [Required]
        public int RegUserId { get; set; }
        [Required]
        public DateTime RegDate { get; set; }

        public bool isActive { get; set; }
        public virtual ICollection<Semester> Semesters { get; set; }
        public virtual ICollection<Scenario> Scenarios { get; set; }

    }
}