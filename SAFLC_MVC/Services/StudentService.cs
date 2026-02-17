using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SAFLC_MVC.Application.Model;
using SAFLC_MVC.Applications.DTO.StudentDTO;
using SAFLC_MVC.Applications.Helpers;
using SAFLC_MVC.Data;
using SAFLC_MVC.Interfaces;

namespace SAFLC_MVC.Services
{
    public class StudentService : BaseService<Student, GetStudentDTO>, IStudentService
    {
        private readonly SaflcDbContext _context;
        public StudentService(IBaseRepository<Student> repository,
            IMapper mapper,
            SaflcDbContext context) : base(repository, mapper)
        {
            _context = context;
        }
        public async Task<ResultResponse<GetStudentDTO>> CreateStudent(CreateStudentDTO dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                dto.CreatedBy = "System";
                var entity = _mapper.Map<Student>(dto);
                //entity.CreatedBy = _currentUser.GetEmail();

                await _repository.SaveAsync(entity);
                await transaction.CommitAsync();

                var studentDTO = _mapper.Map<GetStudentDTO>(entity);

                return ResponseHelper.BuildSuccess(studentDTO, "Student created");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ResponseHelper.BuildFailure<GetStudentDTO>($"Failed: {ex.Message}");
            }
        }

        public async Task<ResultResponse<GetStudentDTO>> UpdateStudent(UpdateStudentDTO dto)
        {
            // 1. Explicit Transaction is needed here because we are hitting TWO tables (Student and AuditLog)
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var entity = await _repository.GetByIdAsync(dto.Id);
                if (entity == null) return ResponseHelper.BuildFailure<GetStudentDTO>("Not found");

                _mapper.Map(dto, entity);
                await _repository.SaveAsync(entity); // Table 1

                // 2. Logic that could fail
                //await _auditLogger.LogUpdateAsync("Student", entity.Id, "Updated profile"); // Table 2

                await transaction.CommitAsync();
                return ResponseHelper.BuildSuccess(_mapper.Map<GetStudentDTO>(entity), $"Student {entity.StudentNo} successfully updated");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                // Log the error and return failure
                return ResponseHelper.BuildFailure<GetStudentDTO>($"Update failed: {ex.Message}");
            }
        }
    }
}
