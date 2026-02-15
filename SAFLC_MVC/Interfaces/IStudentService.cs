using SAFLC_MVC.Applications.DTO.StudentDTO;
using SAFLC_MVC.Applications.Helpers;

namespace SAFLC_MVC.Interfaces
{
    public interface IStudentService : IBaseService<GetStudentDTO>
    {
        Task<ResultResponse<GetStudentDTO>> CreateStudent(CreateStudentDTO createDto);
        Task<ResultResponse<GetStudentDTO>> UpdateStudent(UpdateStudentDTO updateDto);
    }
}
