using AutoMapper;
using SAFLC_MVC.Application.Model;
using SAFLC_MVC.Applications.DTO.StudentDTO;
using SAFLC_MVC.Applications.DTO.SubjectDTO;
using SAFLC_MVC.Applications.Helpers;
using SAFLC_MVC.Applications.Model;
using SAFLC_MVC.Data;
using SAFLC_MVC.Interfaces;

namespace SAFLC_MVC.Services
{
    public class SubjectService : BaseService<Subject, GetSubjectDTO>, ISubjectService
    {
        private readonly SaflcDbContext _context;
        public SubjectService(IBaseRepository<Subject> repository,
            IMapper mapper,
            SaflcDbContext context) : base(context, repository, mapper)
        {
            _context = context;
        }

        public async Task<ResultResponse<GetSubjectDTO>> CreateSubject(CreateSubjectDTO createDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var entity = _mapper.Map<Subject>(createDto);

                await _repository.SaveAsync(entity);
                await transaction.CommitAsync();

                var subjectDTO = _mapper.Map<GetSubjectDTO>(entity);
                return ResponseHelper.BuildSuccess(subjectDTO, "Subject created successfully.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ResponseHelper.BuildFailure<GetSubjectDTO>($"Failed: {ex.Message}");
            }
        }

        public async Task<PaginatedList<GetSubjectDTO>> GetFilteredSubjects(string searchString, int pageSize, int pageNumber = 1)
        {
            var result = await GetAll();
            var query = result.Item ?? new List<GetSubjectDTO>();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim();
                query = query.Where(s => s.SubjectName != null && s.SubjectName.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            var count = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<GetSubjectDTO>(items, count, pageNumber,  pageSize);
        }

        public async Task<ResultResponse<GetSubjectDTO>> UpdateSubject(UpdateStudentDTO updateDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var entity = await _repository.GetByIdAsync(updateDto.Id);
                if (entity == null)
                {
                    return ResponseHelper.BuildFailure<GetSubjectDTO>("Subject not found.");
                }

                _mapper.Map(updateDto, entity);
                await _repository.SaveAsync(entity);

                await transaction.CommitAsync();

                return ResponseHelper.BuildSuccess(_mapper.Map<GetSubjectDTO>(entity), "Subject updated successfully.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ResponseHelper.BuildFailure<GetSubjectDTO>($"Failed: {ex.Message}");
            }
        }
    }
}
