using AutoMapper;
using SAFLC_MVC.Application.Model;
using SAFLC_MVC.Applications.DTO.ClassesDTO;
using SAFLC_MVC.Applications.DTO.SchoolYearDTO;
using SAFLC_MVC.Applications.Helpers;
using SAFLC_MVC.Data;
using SAFLC_MVC.Interfaces;

namespace SAFLC_MVC.Services
{
    public class ClassesService : BaseService<Classes, GetClassesDTO>, IClassesService
    {
        public ClassesService(SaflcDbContext context, IBaseRepository<Classes> repository,
            IMapper mapper) : base(context, repository, mapper)
        {
        }

        public async Task<ResultResponse<GetClassesDTO>> CreateClass(CreateClassesDTO createDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var entity = _mapper.Map<Classes>(createDto);

                await _repository.SaveAsync(entity);
                await transaction.CommitAsync();

                var classesDTO = _mapper.Map<GetClassesDTO>(entity);

                return ResponseHelper.BuildSuccess(classesDTO, $"Class: {classesDTO.ClassName} created ");
            }
            catch (Exception ex) { 
                await transaction.RollbackAsync();
                return ResponseHelper.BuildFailure<GetClassesDTO>($"Failed Creating Class {createDto.ClassName}: {ex.Message}");
            }
        }

        public async Task<List<GetClassesDTO>> GetFilteredClass(string searchString)
        {
            var result = await GetAll();
            var classes = result.Item ?? new List<GetClassesDTO>();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim();
                classes = classes.Where(c => c.ClassName?
                    .Contains(searchString, StringComparison.OrdinalIgnoreCase) ?? false)
                    .ToList();
            }

            return classes;
        }

        public async Task<ResultResponse<GetClassesDTO>> UpdateClass(UpdateClassesDTO updateDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try{
                var entity = await _repository.GetByIdAsync(updateDto.Id);
                if (entity == null) return ResponseHelper.BuildFailure<GetClassesDTO>("Not found");

                _mapper.Map(updateDto, entity);
                await _repository.SaveAsync(entity);

                await transaction.CommitAsync();

                return ResponseHelper.BuildSuccess(_mapper.Map<GetClassesDTO>(entity), $"Class {entity.ClassName} successfully updated");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ResponseHelper.BuildFailure<GetClassesDTO>($"Updating class{updateDto.ClassName} Failed: {ex.Message} ");
            }
        }
    }
}
