namespace aitus.Dto
{
    public class StudentDto
    {
        public int StudentId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
