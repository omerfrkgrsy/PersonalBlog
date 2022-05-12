using PersonalBlog.Core;

namespace PersonalBlog.Repository;

public interface IRepository<T> where T : BaseEntity
{
    IQueryable<T> Table { get; }
    IQueryable<T> TableNoTracking { get; }
    T GetById(object id, bool isDecrypt = false);
    void Insert(T entity, bool isEncrypt = false);        
    void UpdateMatchEntity(T updateEntity, T setEntity, bool isEncrypt = false);//isEncrypt şifreli propertyler var ise "true" atanır.
    void Delete(T entity);
    void Delete(IEnumerable<T> entities);
}