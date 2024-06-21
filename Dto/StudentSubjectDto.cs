namespace aitus.Dto
{
    public class StudentSubjectDto
    {
        public int StudentBarcode { get; set; }
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
        public string GroupName { get; set; }
        public List<SubjectTeacherDto> SubjectTeacher { get; set; }
    }
}
