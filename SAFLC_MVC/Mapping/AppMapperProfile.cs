using AutoMapper;
using SAFLC_MVC.Application.Model;
using SAFLC_MVC.Applications.DTO.StudentDTO;
using SAFLC_MVC.Applications.DTO.SubjectDTO;

namespace SAFLC_MVC.Mapping
{
    public class AppMapperProfile : Profile
    {
        public AppMapperProfile()
        {
            #region Student Mapping

            CreateMap<CreateStudentDTO, Student>().ReverseMap();

            CreateMap<UpdateStudentDTO, Student>().ReverseMap();

            CreateMap<Student, GetStudentDTO>()
                .ReverseMap();

            CreateMap<GetStudentDTO, UpdateStudentDTO>()
                .ReverseMap();

            #endregion

            #region Subject Mapping
            CreateMap<CreateSubjectDTO, Subject>().ReverseMap();

            CreateMap<UpdateSubjectDTO, Subject>().ReverseMap();

            CreateMap<Subject, GetSubjectDTO>()
                .ReverseMap();

            CreateMap<GetSubjectDTO, UpdateSubjectDTO>()
                .ReverseMap();
            #endregion
        }
    }
}
