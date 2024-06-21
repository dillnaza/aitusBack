using aitus.Models;

namespace aitus.Interfaces
{
    public interface IAttendanceRepository
    {
        Attendance GetAttendance(int Id);
        ICollection<Attendance> GetAttendances();
        bool AttendanceExist(int Id);
        double GetAttendancePercent(int studentId, int subjectId);
        ICollection<AttendanceStudent> GetAttendancesByStudentIdAndSubject(int studentId, int subjectId);
        IEnumerable<DateTime> GetAttendanceDatesByGroupIdAndSubjectId(int groupId, int subjectId); 
        IEnumerable<AttendanceStudent> GetAttendancesByGroupIdAndSubjectIdAndDate(int groupId, int subjectId, DateTime date);
        void AddAttendance(Attendance attendance);
        bool Save();
    }
}