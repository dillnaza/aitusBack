using aitus.Models;

namespace aitus.Interfaces
{
    public interface ITeacherRepository
    {
        Teacher GetTeacher(int Id);
        ICollection<Teacher> GetTeachers();
        int GetTeacherBarcode(string Email);
        bool TeacherExists(int Id);
        bool TeachesSubject(int teacherId, int subjectId);
        IEnumerable<Group> GetGroupTeacherGroups(int teacherId);
        IEnumerable<GroupSubject> GetTeacherSubjects(int teacherId);
        Teacher GetTeacherNameBySubjectAndGroup(int subjectId, int groupId);
        Task<Teacher> GetTeacherByEmailAndPasswordAsync(string email, string password);
    }
}
