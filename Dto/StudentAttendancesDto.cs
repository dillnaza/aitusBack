namespace aitus.Dto
{
    public class StudentAttendancesDto
    {
        public int TeacherBarcode { get; set; }
        public string TeacherName { get; set; }
        public string TeacherSurname { get; set; }
        public string SubjectName { get; set; }
        public string GroupName { get; set; }
        public DateTime Date { get; set; }
        public List<StudentAttendanceStatusDto> StudentAttendances { get; set; }
    }
}
