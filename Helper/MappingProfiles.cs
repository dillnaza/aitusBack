using aitus.Dto;
using aitus.Models;
using AutoMapper;

namespace aitus.Helper
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Attendance, AttendanceDto>();
            CreateMap<Group, GroupDto>();
            CreateMap<Teacher, TeacherDto>();
            CreateMap<Student, StudentDto>();
            CreateMap<Subject, SubjectDto>();
            CreateMap<Attendance, StudentAttendanceDto>();
            CreateMap<Student, StudentSubjectDto>();
            CreateMap<Teacher, TeacherSubjectDto>();
        }
    }
}
