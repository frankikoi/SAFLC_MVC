using AutoMapper;
using SAFLC_MVC.Application.Model;
using SAFLC_MVC.Applications.DTO.SchoolYearDTO;
using SAFLC_MVC.Applications.DTO.StudentDTO;
using SAFLC_MVC.Applications.Helpers;
using SAFLC_MVC.Data;
using SAFLC_MVC.Interfaces;

namespace SAFLC_MVC.Services
{
    public class SchoolYearService : BaseService<SchoolYear, GetSchoolYearDTO>, ISchoolYearService
    {
        private readonly SaflcDbContext _context;

        public SchoolYearService(IBaseRepository<SchoolYear> repository,
            IMapper mapper,
            SaflcDbContext context): base(repository, mapper)
        {
            _context = context;
        }

        public async Task<ResultResponse<GetSchoolYearDTO>> CreateSchoolYear(CreateSchoolYearDTO dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var entity = _mapper.Map<SchoolYear>(dto);

                await _repository.SaveAsync(entity);
                await transaction.CommitAsync();

                var schoolYearDTO = _mapper.Map<GetSchoolYearDTO>(entity);

                return ResponseHelper.BuildSuccess(schoolYearDTO, "School year created");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ResponseHelper.BuildFailure<GetSchoolYearDTO>($"Failed: {ex.Message}");
            }
        }

        public async Task<List<GetSchoolYearDTO>> GetFilteredSchoolYears(string searchString)
        {
            var result = await GetAll();
            var schoolYears = result.Item ?? new List<GetSchoolYearDTO>();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim();
                schoolYears = schoolYears.Where(sy =>
                    sy.Year?.Contains(searchString, StringComparison.OrdinalIgnoreCase) ?? false)
                    .ToList();
            }
            return schoolYears;
        }

        public async Task<ResultResponse<GetSchoolYearDTO>> UpdateSchoolYear(UpdateSchoolYearDTO dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var entity = await _repository.GetByIdAsync(dto);
                if (entity == null) return ResponseHelper.BuildFailure<GetSchoolYearDTO>("Not found");

                _mapper.Map(dto, entity);
                await _repository.SaveAsync(entity);

                await transaction.CommitAsync();
                return ResponseHelper.BuildSuccess(_mapper.Map<GetSchoolYearDTO>(entity), $"School Year {entity.Year} scuccessfully updated");
            }
            catch (Exception ex) 
            {
                await transaction.RollbackAsync();

                return ResponseHelper.BuildFailure<GetSchoolYearDTO>($"Update failed: {ex.Message}");
            }
        }
    }
}
