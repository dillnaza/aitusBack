using aitus.Models;

namespace aituss.Interfaces
{
    public interface IStudentRepository
    {
        ICollection<Student> GetStudents();
        Student GetStudent(int Id);
        Student GetStudentByBarcode(int Barcode);
        Student GetStudentBySurname(string Surname);
        Student GetStudentByName(string Name);
        int GetStudentBarcode(string Email);
        bool StudentExists(int Id);
    }
}
