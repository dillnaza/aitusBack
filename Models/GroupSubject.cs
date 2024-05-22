namespace aitus.Models
{
    public class GroupSubject
    {
        public int GroupId { get; set; }
        public int SubjectId { get; set; }
        public Group Group { get; set; }
        public Subject Subject { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}