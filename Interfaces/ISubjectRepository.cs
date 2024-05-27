using aitus.Models;

namespace aitus.Interfaces
{
    public interface ISubjectRepository
    {
        ICollection<Subject> GetSubjects();
        Subject GetSubject(int Id);
        bool SubjectExists(int Id);
    }
}
