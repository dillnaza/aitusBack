using aitus.Interfaces;
using aitus.Models;

namespace aitus.Repositories
{
    public class GroupRepository : IGroupReposotiry
    {
        private readonly DataContext _context;

        public GroupRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Group> GetGroups()
        {
            return _context.Groups.OrderBy(s => s.GroupId).ToList();
        }
        public bool GroupExists(int Id)
        {
            return _context.Groups.Any(s => s.GroupId == Id);
        }
        public ICollection<Student> GetGroupStudents(int GroupId)
        {
            var student = _context.Students.Where(s => s.GroupId == GroupId);
            return student.OrderBy(s => s.StudentId).ToList();
        }

    }
}
