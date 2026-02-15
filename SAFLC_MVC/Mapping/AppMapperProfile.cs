using AutoMapper;
using SAFLC_MVC.Application.Model;
using SAFLC_MVC.Applications.DTO.StudentDTO;

namespace SAFLC_MVC.Mapping
{
    public class AppMapperProfile : Profile
    {
        public AppMapperProfile()
        {
            #region Student Mapping

            CreateMap<Student, CreateStudentDTO>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.RowVersion, opt => opt.MapFrom(src => DateTime.Now))
                .ReverseMap();

            CreateMap<Student, UpdateStudentDTO>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy))
                .ForMember(dest => dest.RowVersion, opt => opt.MapFrom(src => DateTime.Now))
                .ReverseMap();

            CreateMap<Student, GetStudentDTO>()
                .ReverseMap();

            #endregion
        }
    }
}
