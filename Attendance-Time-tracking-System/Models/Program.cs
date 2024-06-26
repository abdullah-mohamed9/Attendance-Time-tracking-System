﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Attendance_Time_tracking_System.Models
{
    public class Program
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Length(maximumLength:50,minimumLength:5, ErrorMessage = "Enter A name between 5 and 50 letter")]
        public string Name { get; set; }
        [DefaultValue(true)]
        public bool status { get; set; } = true;
        
        public string? Description { get; set; }

        public List<IntakeProgram> intakePrograms { get; set; } = new List<IntakeProgram>();
    }
}
