using aitus.Models;

namespace aitus.Interfaces
{
    public interface IGroupReposotiry
    {
        ICollection<Group> GetGroups();
        ICollection<Student> GetGroupStudents(int GroupId);
        bool GroupExists(int Id);
    }
}
