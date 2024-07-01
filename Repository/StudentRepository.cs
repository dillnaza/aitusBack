using aitus.Interfaces;
using aitus.Models;
using Microsoft.EntityFrameworkCore;

namespace aitus.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _context;

        public StudentRepository(DataContext context)
        {
            _context = context;
        }

        public Student GetStudent(int Id)
        {
            return _context.Students.Where(s => s.StudentId == Id).FirstOrDefault();
        }

        public ICollection<Student> GetStudents()
        {
            return _context.Students.OrderBy(s => s.StudentId).ToList();
        }

        public int GetStudentBarcode(string Email)
        {
            var student = _context.Students.FirstOrDefault(s => s.Email == Email);
            if (student == null) return 0;
            string barcode = student.Email[..6];
            _ = int.TryParse(barcode, out int result);
            return result;
        }

        public bool StudentExists(int Id)
        {
            return _context.Students.Any(s => s.StudentId == Id);
        }

        public async Task<Student> GetStudentByEmailAndPasswordAsync(string email, string password)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.Email == email && s.Password == password);
        }
    }
}
