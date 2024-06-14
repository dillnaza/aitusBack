using aitus.Models;

namespace aitus.Interfaces
{
    public interface ITeacherRepository
    {
        Teacher GetTeacher(int Id);
        ICollection<Teacher> GetTeachers();
        int GetTeacherBarcode(string Email);
        bool TeacherExists(int Id);
        public bool TeachesSubject(int teacherId, int subjectId);
        public IEnumerable<Group> GetGroupTeacherGroups(int teacherId);
        public IEnumerable<GroupSubject> GetTeacherSubjects(int teacherId);
        public Teacher GetTeacherNameBySubjectAndGroup(int subjectId, int groupId);
        Task<Teacher> GetTeacherByEmailAndPasswordAsync(string email, string password);
    }
}
