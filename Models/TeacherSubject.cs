namespace aitus.Models
{
    public class TeacherSubject
    {
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        public Teacher Teacher { get; set; }
        public Subject Subject { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}