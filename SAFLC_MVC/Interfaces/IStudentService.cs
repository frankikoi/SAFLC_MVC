using SAFLC_MVC.Applications.DTO.StudentDTO;
using SAFLC_MVC.Applications.Helpers;
using SAFLC_MVC.Applications.Model;

namespace SAFLC_MVC.Interfaces
{
    public interface IStudentService : IBaseService<GetStudentDTO>
    {
        Task<ResultResponse<GetStudentDTO>> CreateStudent(CreateStudentDTO createDto);
        Task<ResultResponse<GetStudentDTO>> UpdateStudent(UpdateStudentDTO updateDto);
        Task<PaginatedList<GetStudentDTO>> GetFilteredStudents(string searchString, int pageNumber = 1);
    }
}
