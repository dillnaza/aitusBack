using aitus.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
    public DbSet<TeacherSubject> TeacherSubjects { get; set; }
    public DbSet<AttendanceStudent> AttendanceStudents { get; set; }
    public DbSet<AttendanceSubject> AttendanceSubjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GroupTeacher>()
            .HasKey(gt => new { gt.GroupId, gt.TeacherId });

        modelBuilder.Entity<GroupTeacher>()
            .HasOne(gt => gt.Group)
            .WithMany(g => g.GroupTeachers)
            .HasForeignKey(gt => gt.GroupId);

        modelBuilder.Entity<GroupTeacher>()
            .HasOne(gt => gt.Teacher)
            .WithMany(t => t.GroupTeachers)
            .HasForeignKey(gt => gt.TeacherId);

        modelBuilder.Entity<GroupSubject>()
            .HasKey(gs => new { gs.GroupId, gs.SubjectId });

        modelBuilder.Entity<GroupSubject>()
            .HasOne(gs => gs.Group)
            .WithMany(g => g.GroupSubjects)
            .HasForeignKey(gs => gs.GroupId);

        modelBuilder.Entity<GroupSubject>()
            .HasOne(gs => gs.Subject)
            .WithMany(s => s.GroupSubjects)
            .HasForeignKey(gs => gs.SubjectId);

        modelBuilder.Entity<TeacherSubject>()
            .HasKey(ts => new { ts.TeacherId, ts.SubjectId });

        modelBuilder.Entity<TeacherSubject>()
            .HasOne(ts => ts.Teacher)
            .WithMany(t => t.TeacherSubjects)
            .HasForeignKey(ts => ts.TeacherId);

        modelBuilder.Entity<TeacherSubject>()
            .HasOne(ts => ts.Subject)
            .WithMany(s => s.TeacherSubjects)
            .HasForeignKey(ts => ts.SubjectId);

        modelBuilder.Entity<AttendanceStudent>()
            .HasKey(asu => new { asu.AttendanceId, asu.StudentId });

        modelBuilder.Entity<AttendanceStudent>()
            .HasOne(asu => asu.Attendance)
            .WithMany(a => a.AttendanceStudents)
            .HasForeignKey(asu => asu.AttendanceId);

        modelBuilder.Entity<AttendanceStudent>()
            .HasOne(asu => asu.Student)
            .WithMany(s => s.AttendanceStudents)
            .HasForeignKey(asu => asu.StudentId);

        modelBuilder.Entity<AttendanceSubject>()
            .HasKey(ass => new { ass.AttendanceId, ass.SubjectId });

        modelBuilder.Entity<AttendanceSubject>()
            .HasOne(ass => ass.Attendance)
            .WithMany(a => a.AttendanceSubjects)
            .HasForeignKey(ass => ass.AttendanceId);

        modelBuilder.Entity<AttendanceSubject>()
            .HasOne(ass => ass.Subject)
            .WithMany(s => s.AttendanceSubjects)
            .HasForeignKey(ass => ass.SubjectId);
    }
}
