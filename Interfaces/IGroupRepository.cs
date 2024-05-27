using aitus.Models;

namespace aitus.Interfaces
{
    public interface IGroupRepository
    {
        ICollection<Group> GetGroups();
        Group GetGroup(int Id);
        ICollection<Student> GetGroupStudents(int GroupId);
        bool GroupExists(int Id);
        IEnumerable<GroupSubject> GetGroupSubjects(int groupId);
        IEnumerable<GroupTeacher> GetGroupTeachers(int groupId);
    }
}
