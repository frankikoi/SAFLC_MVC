using AutoMapper;
using SAFLC_MVC.Application.Model;
using SAFLC_MVC.Applications.DTO.ActivityDTO;
using SAFLC_MVC.Applications.DTO.ClassesDTO;
using SAFLC_MVC.Applications.Helpers;
using SAFLC_MVC.Data;
using SAFLC_MVC.Interfaces;
using System.Diagnostics;

namespace SAFLC_MVC.Services
{
    public class ActivityService : BaseService<Activities, GetActivityDTO>, IActivityService
    {
        public ActivityService(SaflcDbContext context, IBaseRepository<Activities> repository, IMapper mapper) : base(context, repository, mapper)
        {
        }
        public async Task<ResultResponse<GetActivityDTO>> CreateActivity(CreateActivityDTO createDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var entity = _mapper.Map<Activities>(createDto);

                await _repository.SaveAsync(entity);
                await transaction.CommitAsync();

                var activityDTO = _mapper.Map<GetActivityDTO>(entity);

                return ResponseHelper.BuildSuccess(activityDTO, $"Class: {activityDTO.Id} created ");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ResponseHelper.BuildFailure<GetActivityDTO>($"Failed Creating Activity: {ex.Message}");
            }
        }

        public async Task<List<GetActivityDTO>> GetFilteredActivity(string searchString)
        {
            var result = await GetAll();
            var activities = result.Item ?? new List<GetActivityDTO>();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim();
                activities = activities.Where(c => c.Title?
                    .Contains(searchString, StringComparison.OrdinalIgnoreCase) ?? false)
                    .ToList();
            }

            return activities;
        }

        public async Task<ResultResponse<GetActivityDTO>> UpdateActivity(UpdateActivityDTO updateDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var entity = await _repository.GetByIdAsync(updateDto.Id);
                if (entity == null) return ResponseHelper.BuildFailure<GetActivityDTO>("Not found");

                _mapper.Map(updateDto, entity);
                await _repository.SaveAsync(entity);

                await transaction.CommitAsync();

                return ResponseHelper.BuildSuccess(_mapper.Map<GetActivityDTO>(entity), $"Activity {entity.Id} successfully updated");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ResponseHelper.BuildFailure<GetActivityDTO>($"Updating activity: {updateDto.Id} Failed: {ex.Message} ");
            }
        }
    }
}
