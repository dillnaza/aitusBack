using aitus.Interfaces;
using aitus.Models;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                var attendances = _context.AttendanceStudents
                    .Where(a => a.StudentId == studentId)
                    .Include(a => a.Attendance)
                        .ThenInclude(att => att.AttendanceSubjects)
                    .Where(a => a.Attendance.AttendanceSubjects.Any(asub => asub.SubjectId == subjectId))
                    .ToList();
                var totalClasses = attendances.Count;
                var attendedClasses = attendances.Count(a => a.Status == AttendanceStatus.Present || a.Status == AttendanceStatus.Excused);
                if (totalClasses == 0)
                    return 0;
                return (double)attendedClasses / totalClasses * 100;
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine($"Error casting types: {ex.Message}");
                throw;
            }
        }

        public ICollection<AttendanceStudent> GetAttendancesByStudentIdAndSubject(int studentId, int subjectId)
        {
            var attendances = _context.AttendanceStudents
                .Where(at => at.StudentId == studentId)
                .Include(at => at.Attendance)
                    .ThenInclude(a => a.AttendanceSubjects)
                        .ThenInclude(ass => ass.Subject)
                .Where(at => at.Attendance.AttendanceSubjects.Any(ass => ass.SubjectId == subjectId))
                .ToList();
            return attendances;
        }

        public IEnumerable<DateTime> GetAttendanceDatesByGroupIdAndSubjectId(int groupId, int subjectId)
        {
            return _context.AttendanceSubjects
                           .Where(ass => ass.SubjectId == subjectId && ass.Attendance.AttendanceStudents.Any(ast => ast.Student.GroupId == groupId))
                           .Select(ass => ass.Attendance.Date)
                           .ToList();
        }

        public IEnumerable<AttendanceStudent> GetAttendancesByGroupIdAndSubjectIdAndDate(int groupId, int subjectId, DateTime date)
        {
            return _context.AttendanceStudents
                .Include(ass => ass.Student)
                .Include(ass => ass.Attendance)
                .Where(ass => ass.Attendance.Date.Date == date.Date)
                .Where(ass => ass.Attendance.AttendanceSubjects.Any(ats => ats.SubjectId == subjectId))
                .Where(ass => ass.Student.GroupId == groupId)
                .ToList();
        }

        public void AddAttendance(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
        }

        public void DeleteAttendance(Attendance attendance)
        {
            _context.Attendances.Remove(attendance);
        }

        public Attendance GetAttendanceByDateAndSubjectId(DateTime date, int subjectId)
        {
            return _context.AttendanceSubjects
                           .Where(ass => ass.SubjectId == subjectId && ass.Attendance.Date.Date == date.Date)
                           .Select(ass => ass.Attendance)
                           .FirstOrDefault();
        }

        public void AddAttendanceSubject(AttendanceSubject attendanceSubject)
        {
            _context.AttendanceSubjects.Add(attendanceSubject);
        }

        public void AddAttendanceStudent(AttendanceStudent attendanceStudent)
        {
            _context.AttendanceStudents.Add(attendanceStudent);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
