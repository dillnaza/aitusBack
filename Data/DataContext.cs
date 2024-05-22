using aitus.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<GroupTeacher> GroupTeachers { get; set; }
    public DbSet<GroupSubject> GroupSubjects { get; set; }
    public DbSet<TeacherSubject> TeachersSubjects { get; set; }
    public DbSet<AttendanceStudent> AttendanceStudents { get; set; }
    public DbSet<AttendanceSubject> AttendanceSubjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GroupTeacher>()
            .HasKey(gt => new { gt.GroupId, gt.TeacherId });
        modelBuilder.Entity<GroupTeacher>()
            .HasOne(g => g.Group)
            .WithMany(gt => gt.GroupTeachers)
            .HasForeignKey(g => g.GroupId);
        modelBuilder.Entity<GroupTeacher>()
            .HasOne(t => t.Teacher)
            .WithMany(gt => gt.GroupTeachers)
            .HasForeignKey(t => t.TeacherId);

        modelBuilder.Entity<GroupSubject>()
            .HasKey(gs => new { gs.GroupId, gs.SubjectId });
        modelBuilder.Entity<GroupSubject>()
            .HasOne(g => g.Group)
            .WithMany(gs => gs.GroupSubjects)
            .HasForeignKey(g => g.GroupId);
        modelBuilder.Entity<GroupSubject>()
            .HasOne(s => s.Subject)
            .WithMany(gs => gs.GroupSubjects)
            .HasForeignKey(s => s.SubjectId);

        modelBuilder.Entity<TeacherSubject>()
            .HasKey(ts => new { ts.TeacherId, ts.SubjectId });
        modelBuilder.Entity<TeacherSubject>()
            .HasOne(t => t.Teacher)
            .WithMany(ts => ts.TeacherSubjects)
            .HasForeignKey(t => t.TeacherId);
        modelBuilder.Entity<TeacherSubject>()
            .HasOne(s => s.Subject)
            .WithMany(ts => ts.TeacherSubjects)
            .HasForeignKey(s => s.SubjectId);

        modelBuilder.Entity<AttendanceStudent>()
            .HasKey(at => new { at.AttendanceId, at.StudentId });
        modelBuilder.Entity<AttendanceStudent>()
            .HasOne(a => a.Attendance)
            .WithMany(at => at.AttendanceStudents)
            .HasForeignKey(a => a.AttendanceId);
        modelBuilder.Entity<AttendanceStudent>()
            .HasOne(s => s.Student)
            .WithMany(at => at.AttendanceStudents)
            .HasForeignKey(s => s.StudentId);

        modelBuilder.Entity<AttendanceSubject>()
            .HasKey(at => new { at.AttendanceId, at.SubjectId });
        modelBuilder.Entity<AttendanceSubject>()
            .HasOne(a => a.Attendance)
            .WithMany(at => at.AttendanceSubjects)
            .HasForeignKey(a => a.AttendanceId);
        modelBuilder.Entity<AttendanceSubject>()
            .HasOne(s => s.Subject)
            .WithMany(at => at.AttendanceSubjects)
            .HasForeignKey(s => s.SubjectId);
    }
}