using aitus.Interfaces;
using aitus.Models;
using System.Collections.Generic;
using System.Linq;

namespace aitus.Repository
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly DataContext _context;

        public AttendanceRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public Attendance GetAttendance(int Id)
        {
            return _context.Attendances.FirstOrDefault(a => a.AttendanceId == Id);
        }

        public ICollection<Attendance> GetAttendances()
        {
            return _context.Attendances.OrderBy(a => a.AttendanceId).ToList();
        }

        public bool AttendanceExist(int Id)
        {
            return _context.Attendances.Any(a => a.AttendanceId == Id);
        }

        public double GetAttendancePercent(int studentId)
        {
            var totalClasses = _context.AttendanceStudents.Count(a => a.StudentId == studentId);
            var attendedClasses = _context.AttendanceStudents.Count(a => a.StudentId == studentId && (a.Attendance.Status == "Present" || a.Attendance.Status == "Excused"));

            if (totalClasses == 0) return 0;

            return (double)attendedClasses / totalClasses * 100;
        }

        public ICollection<string> GetStudentAttendanceStatuses(int studentId)
        {
            return _context.AttendanceStudents
                .Where(a => a.StudentId == studentId)
                .Select(a => a.Attendance.Status)
                .ToList();
        }
    }
}
