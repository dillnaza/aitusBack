using aitus.Dto;
using aitus.Models;
using AutoMapper;

namespace aitus.Helper
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Student, StudentDto>();
            CreateMap<Group, GroupDto>();
        }
    }
}
