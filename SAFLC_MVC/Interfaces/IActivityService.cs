using SAFLC_MVC.Applications.DTO.ActivityDTO;
using SAFLC_MVC.Applications.DTO.SchoolYearDTO;
using SAFLC_MVC.Applications.Helpers;
using SAFLC_MVC.Applications.Model;

namespace SAFLC_MVC.Interfaces
{
    public interface IActivityService : IBaseService<GetActivityDTO>
    {
        Task<ResultResponse<GetActivityDTO>> CreateActivity(CreateActivityDTO createDto);
        Task<ResultResponse<GetActivityDTO>> UpdateActivity(UpdateActivityDTO updateDto);
        Task<PaginatedList<GetActivityDTO>> GetFilteredActivity(string searchString, int pageNumber = 1);

    }
}
