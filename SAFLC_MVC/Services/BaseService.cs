using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SAFLC_MVC.Applications.Helpers;
using SAFLC_MVC.Data;
using SAFLC_MVC.Interfaces;

namespace SAFLC_MVC.Services
{
    public abstract class BaseService<TEntity, TGetDto> where TEntity : class
    {
        protected readonly SaflcDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected readonly IBaseRepository<TEntity> _repository;

        protected readonly IMapper _mapper;

        protected BaseService(SaflcDbContext context, IBaseRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<ResultResponse<List<TGetDto>>> GetAll()
        {
            var entities = await _repository.GetAllAsync();
            var dtos = _mapper.Map<List<TGetDto>>(entities);
            return ResponseHelper.BuildSuccess(dtos, "Records Retrieved");
        }

        public virtual async Task<ResultResponse<TGetDto>> GetById(object id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return ResponseHelper.BuildFailure<TGetDto>("Record not found.");
            }
            var dto = _mapper.Map<TGetDto>(entity);
            return ResponseHelper.BuildSuccess(dto, "Record Retrieved");
        }

        public virtual async Task<ResultResponse<bool>> DeleteAsync(object id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return ResponseHelper.BuildFailure<bool>("Record not found.");
            }
            _repository.Delete(entity);
            return ResponseHelper.BuildSuccess(true, "Record Deleted successfully");
        }

    }
}
