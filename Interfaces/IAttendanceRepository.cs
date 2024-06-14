using aitus.Models;
using System.Collections.Generic;

namespace aitus.Interfaces
{
    public interface IAttendanceRepository
    {
        Attendance GetAttendance(int Id);
        ICollection<Attendance> GetAttendances();
        bool AttendanceExist(int Id);
        double GetAttendancePercent(int studentId, int subjectId);
        public ICollection<Attendance> GetAttendancesByStudentIdAndSubject(int studentId, int subjectId);

    }
}
