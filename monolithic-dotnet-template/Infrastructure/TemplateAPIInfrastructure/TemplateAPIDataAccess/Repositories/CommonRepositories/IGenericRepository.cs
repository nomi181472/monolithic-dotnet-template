using Microsoft.EntityFrameworkCore.Query;
using RepoResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TemplateAPIDomainModel;

namespace TemplateAPIDataAccess.Repositories.CommonRepositories
{

    public interface IGenericRepository<TEntity,PrimitiveType> where TEntity:Base<PrimitiveType>
    {
        SetterResult Add(TEntity entity, string createdBy);

        SetterResult Update(TEntity entity, string updatedBy);
        SetterResult UpdateMany(TEntity[] entity);
        SetterResult UpdateOnCondition(Expression<Func<TEntity, bool>> filter, Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls);


        SetterResult Delete(TEntity entity);
        SetterResult Delete(PrimitiveType id);
        GetterResult<TEntity> GetById(PrimitiveType id);
        GetterResult<IEnumerable<TEntity>> GetAll();
        GetterResult<IEnumerable<TEntity>> Get(
             Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = ""
            );
        GetterResult<TEntity> GetSingle(
             Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = ""
            );
        GetterResult<IQueryable<TEntity>> GetQueryable();

        //asyncMethod
        Task<SetterResult> AddAsync(TEntity entity, string createdBy);
        Task<SetterResult> UpdateAsync(TEntity entity, string updatedBy);
        Task<SetterResult> DeleteAsync(TEntity entity);
        Task<SetterResult> DeleteAsync(PrimitiveType id);
        Task<GetterResult<TEntity>> GetByIdAsync(PrimitiveType id);


        Task<GetterResult<IEnumerable<TEntity>>> GetAsync(
             Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = ""
            );

        Task<GetterResult<TEntity>> GetSingleAsync(
            Expression<Func<TEntity, bool>> filter = null,
           string includeProperties = ""
           );

    }
}
