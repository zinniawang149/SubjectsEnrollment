using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectsEnrollment.Domain.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        
        [Required]
        public string StudentName { get; set; }

        // Navigation Property
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
