namespace aitus.Dto
{
    public class CreateAttendanceDto
    {
        public DateTime Date { get; set; }
        public int GroupId { get; set; }
        public int SubjectId { get; set; }
    }
}
