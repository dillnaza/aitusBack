using aitus.Models;

namespace aitus.Interfaces
{
    public interface ITeacherRepository
    {
        ICollection<Teacher> GetTeachers();
        Teacher GetTeacher(int Id);
        bool TeacherExists(int Id);
        public bool TeachesSubject(int teacherId, int subjectId);
        Task<Teacher> GetTeacherByEmailAndPasswordAsync(string email, string password);
    }
}
