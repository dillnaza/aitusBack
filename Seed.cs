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
                    new Student {
                        Email = "student1@example.com",
                        Name = "John",
                        Surname = "Doe",
                        Birthday = new DateTime(2000, 1, 1),
                        Password = "password123",
                        GroupId = 1,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        AttendanceStudents = new List<AttendanceStudent>()
                    },
                    new Student {
                        Email = "student2@example.com",
                        Name = "Jane",
                        Surname = "Smith",
                        Birthday = new DateTime(2001, 2, 2),
                        Password = "password456",
                        GroupId = 2,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        AttendanceStudents = new List<AttendanceStudent>()
                    }
                };
                _context.Students.AddRange(students);
            }

            if (!_context.Teachers.Any())
            {
                var teachers = new List<Teacher>
                {
                    new Teacher {
                        Email = "teacher1@example.com",
                        Name = "Alice",
                        Surname = "Johnson",
                        Birthday = new DateTime(1980, 3, 3),
                        Password = "password789",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        TeacherSubjects = new List<TeacherSubject>(),
                        GroupTeachers = new List<GroupTeacher>()
                    },
                    new Teacher {
                        Email = "teacher2@example.com",
                        Name = "Bob",
                        Surname = "Williams",
                        Birthday = new DateTime(1975, 4, 4),
                        Password = "password012",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        TeacherSubjects = new List<TeacherSubject>(),
                        GroupTeachers = new List<GroupTeacher>()
                    }
                };
                _context.Teachers.AddRange(teachers);
            }

            if (!_context.Subjects.Any())
            {
                var subjects = new List<Subject>
                {
                    new Subject {
                        SubjectName = "Calculus 1",
                        AmountOfCredits = 5,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        GroupSubjects = new List<GroupSubject>(),
                        TeacherSubjects = new List<TeacherSubject>(),
                        AttendanceSubjects = new List<AttendanceSubject>()
                    },
                    new Subject {
                        SubjectName = "History of Kazakhstan",
                        AmountOfCredits = 2,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        GroupSubjects = new List<GroupSubject>(),
                        TeacherSubjects = new List<TeacherSubject>(),
                        AttendanceSubjects = new List<AttendanceSubject>()
                    }
                };
                _context.Subjects.AddRange(subjects);
            }

            if (!_context.Groups.Any())
            {
                var groups = new List<Group>
                {
                    new Group {
                        GroupName = "SE-0000",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        Students = new List<Student>(),
                        GroupSubjects = new List<GroupSubject>(),
                        GroupTeachers = new List<GroupTeacher>()
                    },
                    new Group {
                        GroupName = "SE-0001",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        Students = new List<Student>(),
                        GroupSubjects = new List<GroupSubject>(),
                        GroupTeachers = new List<GroupTeacher>()
                    }
                };
                _context.Groups.AddRange(groups);
            }

            if (!_context.Attendances.Any())
            {
                var attendances = new List<Attendance>
                {
                    new Attendance {
                        Date = DateTime.UtcNow,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        AttendanceStudents = new List<AttendanceStudent>(),
                        AttendanceSubjects = new List<AttendanceSubject>()
                    },
                    new Attendance {
                        Date = DateTime.UtcNow,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        AttendanceStudents = new List<AttendanceStudent>(),
                        AttendanceSubjects = new List<AttendanceSubject>()
                    }
                };
                _context.Attendances.AddRange(attendances);
            }

            _context.SaveChanges();
        }
    }
}
