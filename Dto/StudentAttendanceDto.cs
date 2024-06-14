namespace aitus.Dto
{
    public class StudentAttendanceDto
    {
        public int StudentBarcode { get; set; }
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
        public string TeacherName { get; set; }
        public string TeacherSurname { get; set; }
        public string SubjectName { get; set; }
        public string GroupName { get; set; }
        public double AttendancePercent { get; set; }
        public List<AttendanceDto> Attendances { get; set; }
    }
}
