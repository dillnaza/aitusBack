using aitus.Interfaces;
using aitus.Models;

namespace aitus.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly DataContext _context;

        public SubjectRepository(DataContext dataContext)
        {
            _context = dataContext; ;
        }

        public Subject GetSubject(int Id)
        {
            return _context.Subjects.Where(s => s.SubjectId == Id).FirstOrDefault();
        }

        public ICollection<Subject> GetSubjects()
        {
            return _context.Subjects.OrderBy(s => s.SubjectId).ToList();
        }

        public bool SubjectExists(int Id)
        {
            return _context.Subjects.Any(s => s.SubjectId == Id);
        }

    }
}
