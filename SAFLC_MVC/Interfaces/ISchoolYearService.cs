using SAFLC_MVC.Applications.DTO.SchoolYearDTO;
using SAFLC_MVC.Applications.DTO.StudentDTO;
using SAFLC_MVC.Applications.Helpers;
using SAFLC_MVC.Applications.Model;

namespace SAFLC_MVC.Interfaces
{
    public interface ISchoolYearService : IBaseService<GetSchoolYearDTO>
    {
        Task<ResultResponse<GetSchoolYearDTO>> CreateSchoolYear(CreateSchoolYearDTO createDto);
        Task<ResultResponse<GetSchoolYearDTO>> UpdateSchoolYear(UpdateSchoolYearDTO updateDto);
        Task<PaginatedList<GetSchoolYearDTO>> GetFilteredSchoolYears(string searchString, int pageSize = 10, int pageNumber = 1);

    }
}
