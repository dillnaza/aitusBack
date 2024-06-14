using aitus.Interfaces;
using aitus.Models;
using Microsoft.EntityFrameworkCore;
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
        public double GetAttendancePercent(int studentId, int subjectId)
        {
            var attendances = _context.AttendanceStudents
                .Where(a => a.StudentId == studentId)
                .Include(a => a.Attendance)
                .ThenInclude(att => att.AttendanceSubjects)
                .Where(a => a.Attendance.AttendanceSubjects.Any(asub => asub.SubjectId == subjectId))
                .ToList();
            var totalClasses = attendances.Count;
            var attendedClasses = attendances.Count(a => a.Attendance.Status == "Present" || a.Attendance.Status == "Excused");
            Console.WriteLine($"Total classes for student {studentId} in subject {subjectId}: {totalClasses}");
            Console.WriteLine($"Attended classes for student {studentId} in subject {subjectId}: {attendedClasses}");
            if (totalClasses == 0) return 0;
            return (double)attendedClasses / totalClasses * 100;
        }

        public ICollection<Attendance> GetAttendancesByStudentIdAndSubject(int studentId, int subjectId)
        {
            return _context.Attendances
                .Include(a => a.AttendanceStudents)
                .ThenInclude(ass => ass.Student)
                .Include(a => a.AttendanceSubjects)
                .ThenInclude(asub => asub.Subject)
                .Where(a => a.AttendanceStudents.Any(ass => ass.StudentId == studentId) && a.AttendanceSubjects.Any(asub => asub.SubjectId == subjectId))
                .ToList();
        }
    }
}
