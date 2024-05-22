namespace aitus.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public int AmountOfCredits { get; set; }
        public ICollection<GroupSubject> GroupSubjects { get; set; }
        public ICollection<TeacherSubject> TeacherSubjects { get; set; }
        public ICollection<AttendanceSubject> AttendanceSubjects { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}