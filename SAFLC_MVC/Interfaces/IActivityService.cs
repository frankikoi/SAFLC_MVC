using SAFLC_MVC.Applications.DTO.ActivityDTO;
using SAFLC_MVC.Applications.DTO.SchoolYearDTO;
using SAFLC_MVC.Applications.Helpers;

namespace SAFLC_MVC.Interfaces
{
    public interface IActivityService : IBaseService<GetActivityDTO>
    {
        Task<ResultResponse<GetActivityDTO>> CreateActivity(CreateActivityDTO createDto);
        Task<ResultResponse<GetActivityDTO>> UpdateActivity(UpdateActivityDTO updateDto);
        Task<List<GetActivityDTO>> GetFilteredActivity(string searchString);
    }
}
