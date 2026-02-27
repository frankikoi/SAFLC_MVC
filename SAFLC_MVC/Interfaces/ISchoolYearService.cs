using SAFLC_MVC.Applications.DTO.SchoolYearDTO;
using SAFLC_MVC.Applications.DTO.StudentDTO;
using SAFLC_MVC.Applications.Helpers;

namespace SAFLC_MVC.Interfaces
{
    public interface ISchoolYearService : IBaseService<GetSchoolYearDTO>
    {
        Task<ResultResponse<GetSchoolYearDTO>> CreateSchoolYear(CreateSchoolYearDTO createDto);
        Task<ResultResponse<GetSchoolYearDTO>> UpdateSchoolYear(UpdateSchoolYearDTO updateDto);
        Task<List<GetSchoolYearDTO>> GetFilteredSchoolYears(string searchString);

    }
}
