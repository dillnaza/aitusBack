namespace aitus.Dto
{
    public class StudentAttendanceDto
    {
        public int StudentBarcode { get; set; }
        public string GroupName { get; set; }
        public List<SubjectTeacherDto> SubjectTeacher { get; set; }
    }
}
