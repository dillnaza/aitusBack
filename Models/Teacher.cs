using System.ComponentModel.DataAnnotations;

namespace aitus.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public string Password { get; set; } = string.Empty;
        public ICollection<TeacherSubject> TeacherSubjects { get; set; }
        public ICollection<GroupTeacher> GroupTeachers { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}