using Microsoft.EntityFrameworkCore.Storage;
using PersonalBlog.Core;
using System.Linq.Expressions;

namespace PersonalBlog.Repository.EntityFramework;

public interface IRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);
    Task<bool> InsertAsync(T entity, bool isEncrypt = false);        
    Task<bool> UpdateMatchEntity(T updateEntity,int key, bool isEncrypt = false);
    Task<bool> DeleteAsync(T entity);
    Task<bool> DeleteAsync(IEnumerable<T> entities);
    Task<int> SaveAsync();
    Task<bool> AnyAsync(Expression<Func<T, bool>> where = null);
    IQueryable<T> All();
    IDbContextTransaction BeginTransaction();
}