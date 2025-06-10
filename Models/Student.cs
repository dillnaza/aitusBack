using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace aitus.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public string Password { get; set; } = string.Empty;
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public ICollection<AttendanceStudent> AttendanceStudents { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
