namespace aitus.Dto
{
    public class TeacherSubjectDto
    {
        public int TeacherBarcode { get; set; }
        public string TeacherName { get; set; }
        public string TeacherSurname { get; set; }
        public List<SubjectGroupDto> SubjectGroup { get; set; }
    }
}
