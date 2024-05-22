namespace aitus.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public ICollection<Student> Students { get; set; }
        public ICollection<GroupSubject> GroupSubjects { get; set; }
        public ICollection<GroupTeacher> GroupTeachers { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}