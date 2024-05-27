namespace aitus.Dto
{
    public class AttendanceDto
    {
        public int AttendanceId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
