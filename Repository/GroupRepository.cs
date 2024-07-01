using aitus.Interfaces;
using aitus.Models;
using Microsoft.EntityFrameworkCore;

namespace aitus.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DataContext _context;

        public GroupRepository(DataContext context)
        {
            _context = context;
        }

        public Group GetGroup(int Id)
        {
            return _context.Groups.Where(g => g.GroupId == Id).FirstOrDefault();
        }

        public ICollection<Group> GetGroups()
        {
            return _context.Groups.OrderBy(g => g.GroupId).ToList();
        }

        public ICollection<Student> GetGroupStudents(int GroupId)
        {
            var student = _context.Students.Where(g => g.GroupId == GroupId);
            return student.OrderBy(s => s.StudentId).ToList();
        }

        public bool GroupExists(int Id)
        {
            return _context.Groups.Any(g => g.GroupId == Id);
        }

        public IEnumerable<GroupSubject> GetGroupSubjects(int groupId)
        {
            return _context.GroupSubjects
                           .Where(gs => gs.GroupId == groupId)
                           .Include(gs => gs.Subject)
                           .ToList();
        }

        public IEnumerable<GroupTeacher> GetGroupTeachers(int groupId)
        {
            return _context.GroupTeachers
                           .Where(gt => gt.GroupId == groupId)
                           .Include(gt => gt.Teacher)
                           .ToList();
        }
    }
}
