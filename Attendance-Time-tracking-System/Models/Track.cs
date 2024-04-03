﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Time_tracking_System.Models
{
    public class Track
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool Status { get; set; } = true;
        [Required]
        [ForeignKey("InstructorNavigation")]
        public int SupervisorID { get; set; }
        [Required]
        [ForeignKey("ProgramNavigation")]
        public int ProgramID { get; set; }
        [Range(maximum:50,minimum:10 , ErrorMessage ="enter number between 10 and 50") ]
        public int? Capacity { get; set; }

        [ForeignKey("SupervisorID")]
        public Instructor InstructorNavigation { get; set; }
        [ForeignKey("ProgramID")]
        public Program ProgramNavigation { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();
        public List<WorksIn> Works { get; set; } = new List<WorksIn>();
    }
}
