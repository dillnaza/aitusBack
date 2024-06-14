using aitus.Models;

namespace aitus.Interfaces
{
    public interface ISubjectRepository
    {
        Subject GetSubject(int Id);
        ICollection<Subject> GetSubjects();
        bool SubjectExists(int Id);
    }
}
