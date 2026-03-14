using SAFLC_MVC.Applications.DTO.ClassesDTO;
using SAFLC_MVC.Applications.DTO.SchoolYearDTO;
using SAFLC_MVC.Applications.Helpers;

namespace SAFLC_MVC.Interfaces
{
    public interface IClassesService : IBaseService<GetClassesDTO>
    {
        Task<ResultResponse<GetClassesDTO>> CreateClass(CreateClassesDTO createDto);
        Task<ResultResponse<GetClassesDTO>> UpdateClass(UpdateClassesDTO updateDto);
        Task<List<GetClassesDTO>> GetFilteredClass(string searchString);
    }
}
