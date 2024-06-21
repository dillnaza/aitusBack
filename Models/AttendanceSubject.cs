namespace aitus.Models
{
    public class AttendanceSubject
    {
        public int AttendanceId { get; set; }
        public int SubjectId { get; set; }
        public Attendance Attendance { get; set; }
        public Subject Subject { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}