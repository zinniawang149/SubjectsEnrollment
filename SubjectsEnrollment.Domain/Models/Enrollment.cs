using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectsEnrollment.Domain.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }

        [Required]
        public string Status { get; set; }

        // Navigation Properties
        public Student Student { get; set; }
        public Subject Subject { get; set; }


    }
}
