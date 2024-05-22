namespace aitus.Models
{
    public class GroupTeacher
    {
        public int GroupId { get; set; }
        public int TeacherId { get; set; }
        public Group Group { get; set; }
        public Teacher Teacher { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}