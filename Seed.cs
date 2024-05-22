using aitus.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aitus.Data
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedDataContext()
        {
            if (!_context.Students.Any())
            {
                var students = new List<Student>
                {
                    new() {
                        Email = "student1@example.com",
                        Name = "John",
                        Surname = "Doe",
                        Birthday = new DateTime(2000, 1, 1),
                        Password = "password123",
                        GroupId = 1,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        AttendanceStudents = []
                    },
                    new() {
                        Email = "student2@example.com",
                        Name = "Jane",
                        Surname = "Smith",
                        Birthday = new DateTime(2001, 2, 2),
                        Password = "password456",
                        GroupId = 2,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        AttendanceStudents = []
                    }
                };
                _context.Students.AddRange(students);
            }

            if (!_context.Teachers.Any())
            {
                var teachers = new List<Teacher>
                {
                    new() {
                        Email = "teacher1@example.com",
                        Name = "Alice",
                        Surname = "Johnson",
                        Birthday = new DateTime(1980, 3, 3),
                        Password = "password789",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        TeacherSubjects = [],
                        GroupTeachers = []
                    },
                    new()  {
                        Email = "teacher2@example.com",
                        Name = "Bob",
                        Surname = "Williams",
                        Birthday = new DateTime(1975, 4, 4),
                        Password = "password012",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        TeacherSubjects =[],
                        GroupTeachers = []
                    }
                };
                _context.Teachers.AddRange(teachers);
            }

            if (!_context.Subjects.Any())
            {
                var subjects = new List<Subject>
                {
                    new() {
                        SubjectName = "Mathematics",
                        AmountOfCredits = 3,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        GroupSubjects = [],
                        TeacherSubjects = [],
                        AttendanceSubjects = []
                    },
                    new() {
                        SubjectName = "History",
                        AmountOfCredits = 2,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        GroupSubjects = [],
                        TeacherSubjects = [],
                        AttendanceSubjects = []
                    }
                };
                _context.Subjects.AddRange(subjects);
            }

            if (!_context.Groups.Any())
            {
                var groups = new List<Group>
                {
                    new() {
                        GroupName = "Group A",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        Students = [],
                        GroupSubjects = [],
                        GroupTeachers = []
                    },
                    new Group
                    {
                        GroupName = "Group B",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        Students = [],
                        GroupSubjects = [],
                        GroupTeachers = []
                    }
                };
                _context.Groups.AddRange(groups);
            }

            if (!_context.Attendances.Any())
            {
                var attendances = new List<Attendance>
                {
                    new() {
                        Date = DateTime.UtcNow,
                        Status = "Present",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        AttendanceStudents = [],
                        AttendanceSubjects = []
                    },
                    new() {
                        Date = DateTime.UtcNow,
                        Status = "Absent",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        AttendanceStudents = [],
                        AttendanceSubjects = []
                    }
                };
                _context.Attendances.AddRange(attendances);
            }

            _context.SaveChanges();
        }
    }
}