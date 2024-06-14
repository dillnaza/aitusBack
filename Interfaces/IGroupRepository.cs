using aitus.Models;

namespace aitus.Interfaces
{
    public interface IGroupRepository
    {
        Group GetGroup(int Id);
        ICollection<Group> GetGroups();
        bool GroupExists(int Id);
        ICollection<Student> GetGroupStudents(int GroupId);
        IEnumerable<GroupSubject> GetGroupSubjects(int groupId);
        IEnumerable<GroupTeacher> GetGroupTeachers(int groupId);
    }
}
