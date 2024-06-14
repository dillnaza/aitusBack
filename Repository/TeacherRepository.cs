using aitus.Interfaces;
using aitus.Models;
using Microsoft.EntityFrameworkCore;

namespace aitus.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly DataContext _context;

        public TeacherRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public Teacher GetTeacher(int Id)
        {
            return _context.Teachers.Where(t => t.TeacherId == Id).FirstOrDefault();
        }

        public ICollection<Teacher> GetTeachers()
        {
            return _context.Teachers.OrderBy(t => t.TeacherId).ToList();
        }

        public bool TeacherExists(int Id)
        {
            return _context.Teachers.Any(t => t.TeacherId == Id);
        }

        public int GetTeacherBarcode(string Email)
        {
            var teacher = _context.Teachers.FirstOrDefault(s => s.Email == Email);
            if (teacher == null) return 0;
            string barcode = teacher.Email[..6];
            _ = int.TryParse(barcode, out int result);
            return result;
        }

        public bool TeachesSubject(int teacherId, int subjectId)
        {
            return _context.TeachersSubjects
                           .Any(ts => ts.TeacherId == teacherId && ts.SubjectId == subjectId);
        }

        public IEnumerable<GroupSubject> GetTeacherSubjects(int teacherId)
        {
            return _context.GroupTeachers
                           .Where(gt => gt.TeacherId == teacherId)
                           .SelectMany(gt => gt.Group.GroupSubjects)
                           .Include(gs => gs.Subject)
                           .ToList();
        }

        public IEnumerable<Group> GetGroupTeacherGroups(int teacherId)
        {
            return _context.GroupTeachers
                           .Where(gt => gt.TeacherId == teacherId)
                           .Select(gt => gt.Group)
                           .ToList();
        }

        public Teacher GetTeacherNameBySubjectAndGroup(int subjectId, int groupId)
        {
            return _context.GroupSubjects
                .Where(gs => gs.SubjectId == subjectId && gs.GroupId == groupId)
                .SelectMany(gs => gs.Group.GroupTeachers)
                .Select(gt => gt.Teacher)
                .FirstOrDefault();
        }

        public async Task<Teacher> GetTeacherByEmailAndPasswordAsync(string email, string password)
        {
            return await _context.Teachers.FirstOrDefaultAsync(t => t.Email == email && t.Password == password);
        }
    }
}
