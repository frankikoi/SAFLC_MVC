using SAFLC_MVC.Applications.Helpers;

namespace SAFLC_MVC.Interfaces
{
    public interface IBaseService<TGetDto>
    {
        Task<ResultResponse<List<TGetDto>>> GetAll();

        Task<ResultResponse<TGetDto>> GetById(object id);

        Task<ResultResponse<bool>> DeleteAsync(object id);
    }
}
