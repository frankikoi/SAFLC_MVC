using SAFLC_MVC.Applications.DTO.StudentDTO;
using SAFLC_MVC.Applications.DTO.SubjectDTO;
using SAFLC_MVC.Applications.Helpers;
using SAFLC_MVC.Applications.Model;

namespace SAFLC_MVC.Interfaces
{
    public interface ISubjectService : IBaseService<GetSubjectDTO>
    {
        Task<ResultResponse<GetSubjectDTO>> CreateSubject(CreateSubjectDTO createDto);
        Task<ResultResponse<GetSubjectDTO>> UpdateSubject(UpdateStudentDTO updateDto);
        Task<PaginatedList<GetSubjectDTO>> GetFilteredSubjects(string searchString, int pageSize, int pageNumber = 1);
    }
}
