using AutoMapper;
using T2507E_ASP.DTOs.Student;
using T2507E_ASP.Entities;

namespace T2507E_ASP.Mappings;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<Student, StudentResponse>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name));
        CreateMap<CreateStudentRequest, Student>()
            .ForMember(dest=> dest.StudentCode, 
                    opt => opt.MapFrom(src => src.Code.Trim().ToUpperInvariant()));
        CreateMap<UpdateStudentRequest, Student>();
    }
}