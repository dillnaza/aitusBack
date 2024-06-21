namespace aitus.Models
{
    public class AttendanceStudent
    {
        public int AttendanceId { get; set; }
        public int StudentId { get; set; }
        public AttendanceStatus Status { get; set; }
        public Attendance Attendance { get; set; }
        public Student Student { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}