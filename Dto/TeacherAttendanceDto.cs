namespace aitus.Dto
{
    public class TeacherAttendanceDto
    {
        public int TeacherBarcode { get; set; }
        public string TeacherName { get; set; }
        public string TeacherSurname { get; set; }
        public string SubjectName { get; set; }
        public string GroupName { get; set; }
        public List<DateTime> AttendanceDates { get; set; }
    }
}
