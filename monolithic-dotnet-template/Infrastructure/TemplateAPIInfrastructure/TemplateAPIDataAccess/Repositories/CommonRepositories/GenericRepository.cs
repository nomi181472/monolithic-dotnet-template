using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RepoResult;
using System.Linq.Expressions;

using TemplateAPIDataAccess.DBContext;
using TemplateAPIDomainModel;

namespace TemplateAPIDataAccess.Repositories.CommonRepositories
{
    public class GenericRepository<TEntity, PrimitiveType> : IGenericRepository<TEntity, PrimitiveType> where TEntity : Base<PrimitiveType>
    {
        private readonly TemplateDBContext _templateDBContext;

        internal DbSet<TEntity> _dbSet;

        public GenericRepository(TemplateDBContext _context)
        {
            _templateDBContext = _context;
            _dbSet = _templateDBContext.Set<TEntity>();
        }


        public virtual SetterResult Add(TEntity entity, string createdBy)
        {
            try
            {
                var currentDate = DateTime.UtcNow;
                entity.CreatedBy = createdBy;
                entity.UpdatedBy = createdBy;
                entity.UpdatedDate = currentDate;
                entity.CreatedDate = currentDate;
                entity.IsActive = true;
                entity.IsArchived = false;

                _dbSet.Add(entity);
                return new SetterResult() { IsException = false, result = true, Message = ResultMessage.Success };
            }
            catch (Exception e)
            {
                return new SetterResult() { IsException = true, result = false, Message = e.ToString(), };

            }

        }

        public virtual async Task<SetterResult> AddAsync(TEntity entity, string createdBy)
        {
            try
            {
                var currentDate = DateTime.UtcNow;
                entity.CreatedBy = createdBy;
                entity.UpdatedBy = createdBy;
                entity.UpdatedDate = currentDate;
                entity.CreatedDate = currentDate;
                entity.IsActive = true;
                entity.IsArchived = false;
                await _dbSet.AddAsync(entity);
                return new SetterResult() { IsException = false, result = true, Message = ResultMessage.Success };
            }
            catch (Exception e)
            {
                return new SetterResult() { IsException = true, result = false, Message = e.ToString() };

            }
        }

        public virtual SetterResult Delete(TEntity entity)
        {
            try
            {
                _dbSet.Remove(entity);
                return new SetterResult() { IsException = false, result = true, Message = ResultMessage.Success };
            }
            catch (Exception e)
            {
                return new SetterResult() { IsException = true, result = false, Message = e.ToString() };

            }
        }

        public virtual SetterResult Delete(PrimitiveType id)
        {
            try
            {
                var data = _dbSet.Find(id);
                if (data != null)
                {
                    _dbSet.Remove(data);
                    return new SetterResult() { IsException = false, result = true, Message = ResultMessage.Success };
                }
                else
                {
                    return new SetterResult() { IsException = false, result = false, Message = ResultMessage.NotFound };
                }


            }
            catch (Exception e)
            {
                return new SetterResult() { IsException = true, result = false, Message = e.ToString() };

            }
        }

        public virtual async Task<SetterResult> DeleteAsync(TEntity entity)
        {
            try
            {

                _dbSet.Remove(entity);
                return new SetterResult() { IsException = false, result = true, Message = ResultMessage.Success };


            }
            catch (Exception e)
            {
                return new SetterResult() { IsException = false, result = false, Message = e.ToString() };

            }
        }

        public virtual async Task<SetterResult> DeleteAsync(PrimitiveType id)
        {
            try
            {
                var data = await _dbSet.FindAsync(id);
                if (data != null)
                {
                    _dbSet.Remove(data);
                    return new SetterResult() { result = true, IsException = false, Message = ResultMessage.Success };
                }
                else
                {
                    return new SetterResult() { result = false, IsException = false, Message = ResultMessage.NotFound };
                }


            }
            catch (Exception e)
            {
                return new SetterResult() { IsException = true, result = false, Message = e.ToString() };

            }
        }



        public virtual GetterResult<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            try
            {
                GetterResult<IEnumerable<TEntity>> getterResult = new GetterResult<IEnumerable<TEntity>>();
                getterResult.Message = ResultMessage.Success;
                getterResult.Status = true;
                IQueryable<TEntity> query = _dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    getterResult.Data = orderBy(query).ToList();
                }
                else
                {
                    getterResult.Data = query.ToList();
                }

                return getterResult;
            }
            catch (Exception e)
            {

                return new GetterResult<IEnumerable<TEntity>>() { Message = e.ToString(), Status = false, Data = null };
            }
        }
        public virtual async Task<GetterResult<IEnumerable<TEntity>>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {

            try
            {
                GetterResult<IEnumerable<TEntity>> getterResult = new GetterResult<IEnumerable<TEntity>>();
                getterResult.Message = ResultMessage.Success;
                getterResult.Status = true;
                IQueryable<TEntity> query = _dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    getterResult.Data = await orderBy(query).ToListAsync();
                }
                else
                {
                    getterResult.Data = await query.ToListAsync();
                }

                return getterResult;
            }
            catch (Exception e)
            {

                return new GetterResult<IEnumerable<TEntity>>() { Message = e.ToString(), Status = false, Data = null };
            }
        }

        public virtual GetterResult<IEnumerable<TEntity>> GetAll()
        {
            try
            {
                GetterResult<IEnumerable<TEntity>> getterResult = new GetterResult<IEnumerable<TEntity>>();
                getterResult.Message = ResultMessage.Success;
                getterResult.Status = true;
                getterResult.Data = _dbSet.AsEnumerable();
                return getterResult;
            }
            catch (Exception e)
            {

                return new GetterResult<IEnumerable<TEntity>>() { Message = e.Message, Status = false, Data = null };
            }

        }





        public virtual GetterResult<TEntity> GetById(PrimitiveType id)
        {
            try
            {
                var data = _dbSet.Find(id);
                GetterResult<TEntity> getterResult = new GetterResult<TEntity>();
                getterResult.Message = ResultMessage.Success;
                getterResult.Status = true;
                getterResult.Data = data;
                return getterResult;

            }
            catch (Exception e)
            {

                return new GetterResult<TEntity>() { Message = e.Message, Status = false, Data = null };
            }
        }

        public virtual async Task<GetterResult<TEntity>> GetByIdAsync(PrimitiveType id)
        {
            try
            {
                var data = await _dbSet.FindAsync(id);
                GetterResult<TEntity> getterResult = new GetterResult<TEntity>();
                getterResult.Message = ResultMessage.Success;
                getterResult.Status = true;
                getterResult.Data = data;
                return getterResult;

            }
            catch (Exception e)
            {

                return new GetterResult<TEntity>() { Message = e.Message, Status = false, Data = null };
            }

        }

        public virtual GetterResult<IQueryable<TEntity>> GetQueryable()
        {
            try
            {

                GetterResult<IQueryable<TEntity>> getterResult = new GetterResult<IQueryable<TEntity>>();
                getterResult.Message = ResultMessage.Success;
                getterResult.Status = true;
                getterResult.Data = _dbSet.AsQueryable();
                return getterResult;

            }
            catch (Exception e)
            {

                return new GetterResult<IQueryable<TEntity>>() { Message = e.Message, Status = false, Data = null };
            }
        }

        public virtual SetterResult Update(TEntity entity, string updatedBy)
        {
            try
            {
                entity.UpdatedDate = DateTime.UtcNow;
                entity.UpdatedBy = updatedBy;
                _dbSet.Update(entity);
                return new SetterResult() { Message = ResultMessage.Success, result = true, IsException = false };
            }
            catch (Exception e)
            {
                return new SetterResult() { Message = e.ToString(), result = false, IsException = true };
            }
        }
        public virtual SetterResult UpdateOnCondition(Expression<Func<TEntity, bool>> filter, Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls)
        {
            try
            {
                IQueryable<TEntity> query = _dbSet;
                int rowsEffected = query.Where(filter).ExecuteUpdate(setPropertyCalls);

                return new SetterResult() { Message = $"{ResultMessage.Success}.RowsEffected:{rowsEffected}", result = true, IsException = false };
            }
            catch (Exception e)
            {
                return new SetterResult() { Message = e.ToString(), result = false, IsException = true };
            }
        }
        public virtual SetterResult UpdateMany(TEntity[] entity)
        {
            try
            {

                _dbSet.UpdateRange(entity);
                return new SetterResult() { Message = ResultMessage.Success, result = true, IsException = false };
            }
            catch (Exception e)
            {
                return new SetterResult() { Message = e.ToString(), result = false, IsException = true };
            }
        }

        public virtual async Task<SetterResult> UpdateAsync(TEntity entity, string updatedBy)
        {
            try
            {
                entity.UpdatedDate = DateTime.UtcNow;
                entity.UpdatedBy = updatedBy;
                _dbSet.Update(entity);

                return new SetterResult() { IsException = false, result = true, Message = ResultMessage.Success, };
            }
            catch (Exception e)
            {
                return new SetterResult() { IsException = true, result = false, Message = e.ToString(), };
            }
        }

        public GetterResult<TEntity> GetSingle(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
        {
            try
            {
                GetterResult<TEntity> getterResult = new GetterResult<TEntity>();
                getterResult.Message = ResultMessage.Success;
                getterResult.Status = true;
                IQueryable<TEntity> query = _dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                getterResult.Data = query.FirstOrDefault();

                return getterResult;
            }
            catch (Exception e)
            {

                return new GetterResult<TEntity>() { Message = e.ToString(), Status = false, Data = null };
            }
        }

        public async Task<GetterResult<TEntity>> GetSingleAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
        {
            try
            {
                GetterResult<TEntity> getterResult = new GetterResult<TEntity>();
                getterResult.Message = ResultMessage.Success;
                getterResult.Status = true;
                IQueryable<TEntity> query = _dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                getterResult.Data = await query.FirstOrDefaultAsync();

                return getterResult;
            }
            catch (Exception e)
            {

                return new GetterResult<TEntity>() { Message = e.ToString(), Status = false, Data = null };
            }
        }

    }
}
