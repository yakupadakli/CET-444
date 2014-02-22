﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using CollaborativeLearning.Entities;

namespace CollaborativeLearning.WebUI.Models
{
    public class WorkWithDueDate
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

        public int SemesterID { get; set; }
        public virtual Semester Semester { get; set; }

        public int WorkID { get; set; }
        public virtual Work Work { get; set; }

        public int ScenarioID { get; set; }
        public virtual Scenario Scenario { get; set; }

        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        public virtual ICollection<Scenario> Scenarios { get; set; }
        public virtual ICollection<GroupWork> GroupWorks { get; set; }
    }
}