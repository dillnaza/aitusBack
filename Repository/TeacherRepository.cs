using aitus.Interfaces;
using aitus.Models;

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

        public bool TeachesSubject(int teacherId, int subjectId)
        {
            return _context.TeachersSubjects
                           .Any(ts => ts.TeacherId == teacherId && ts.SubjectId == subjectId);
        }
    }
}
