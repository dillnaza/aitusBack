using aituss.Interfaces;
using aitus.Models;
using Microsoft.EntityFrameworkCore;

namespace aituss.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _context;

        public StudentRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Student> GetStudents()
        {
            return _context.Students.OrderBy(s => s.StudentId).ToList();
        }

        public Student GetStudent(int Id)
        {
            return _context.Students.Where(s => s.StudentId == Id).FirstOrDefault();
        }

        public Student GetStudentByBarcode(int Barcode)
        {
            return _context.Students.Where(s => s.StudentId == Barcode).FirstOrDefault();
        }

        public Student GetStudentBySurname(string Surname)
        {
            return _context.Students.Where(s => s.Surname == Surname).FirstOrDefault();
        }

        public Student GetStudentByName(string Name)
        {
            return _context.Students.Where(s => s.Name == Name).FirstOrDefault();
        }

        public int GetStudentBarcode(string Email)
        {
            var student = _context.Students.FirstOrDefault(s => s.Email == Email);
            if (student == null) return 0;
            string barcode = student.Email.Substring(0, 6);
            int.TryParse(barcode, out int result);
            return result;
        }

        public bool StudentExists(int Id)
        {
            return _context.Students.Any(s => s.StudentId == Id);
        }

        public async Task<Student> GetStudentByEmailAndPasswordAsync(string email, string password)
        {
            // Найти студента по электронной почте и паролю
            return await _context.Students.FirstOrDefaultAsync(s => s.Email == email && s.Password == password);
        }
    }
}
