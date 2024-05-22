namespace aitus.Models
{
    public class Attendance
    {
        public int AttendanceId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;
        public ICollection<AttendanceStudent> AttendanceStudents { get; set; }
        public ICollection<AttendanceSubject> AttendanceSubjects { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}