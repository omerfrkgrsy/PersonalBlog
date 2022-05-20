using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using PersonalBlog.Core;
using PersonalBlog.Core.Security;
using PersonalBlog.DataAccess.DbContext;
using PersonalBlog.DataAccess.PartialEntities;

namespace PersonalBlog.Repository.EntityFramework;

public class EfRepository<T>: IRepository<T> where T : BaseEntity
{
    private readonly PersonalBlogDbContext _context;
    private DbSet<T> _entities;
    private readonly IEncryption _encryption;
    protected virtual DbSet<T> Entities => _entities ?? (_entities = _context.Set<T>());

    public EfRepository(PersonalBlogDbContext context, IEncryption encryption)
    {
        _context = context;
        _entities = context.Set<T>();
        _encryption = encryption;
    }

    public IDbContextTransaction BeginTransaction()
    {
        return _context.Database.BeginTransaction();
    }

    public async Task<int> SaveAsync()
    {
        return await this._context.SaveChangesAsync();
    }

    public async Task<bool> InsertAsync(T entity, bool isEncrypt=false)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        if (isEncrypt)
        {
            entity = EncryptEntityFields(entity, _context);
        }
        entity.UsedTime = DateTime.Now;

        _entities.AddAsync(entity);

        return await SaveAsync()>-1;
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        Entities.Remove(entity);
        return await SaveAsync() > -1;
    }

    public async Task<bool> DeleteAsync(IEnumerable<T> entities)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));

        foreach (var entity in entities)
            Entities.Remove(entity);
                    
        return await SaveAsync() > -1;
    }

    public IQueryable<T> All()
    {
        return Entities.AsQueryable();
    }

    public async Task<bool> UpdateMatchEntity(T updateEntity,int key, bool isEncrypt = false)
    {
        T setEntity = await GetByIdAsync(key);

        if (setEntity == null)
            throw new ArgumentNullException(nameof(setEntity));

        if (updateEntity == null)
            throw new ArgumentNullException(nameof(updateEntity));
        

        _context.Entry(updateEntity).CurrentValues.SetValues(setEntity);

        foreach (var property in _context.Entry(setEntity).Properties)
        {
            if (property.CurrentValue == null) { _context.Entry(updateEntity).Property(property.Metadata.Name).IsModified = false; }
        }
        if (isEncrypt)
        {
            updateEntity = UpdateEncryptedEntityFieldIfChange(updateEntity);
        }

        return await SaveAsync() > -1;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        T entity =await Entities.FindAsync(id);
        return entity;
    }
    
    public async Task<bool> AnyAsync(Expression<Func<T, bool>> where = null)
    {
        return await _context.Set<T>().AnyAsync(where);
    }

    public virtual T EncryptEntityFields(T entity, PersonalBlogDbContext dbContext)
    {
        MetadataTypeAttribute[] metadataTypes = entity.GetType().GetCustomAttributes(true).OfType<MetadataTypeAttribute>().ToArray();
        foreach (MetadataTypeAttribute metadata in metadataTypes)
        {
            System.Reflection.PropertyInfo[] properties = metadata.MetadataClassType.GetProperties();
            //Metadata atanmış entity'nin tüm propertyleri tek tek alınır.
            foreach (System.Reflection.PropertyInfo pi in properties)
            {
                //Eğer ilgili property ait CryptoData flag'i var ise ilgili deger encrypt edilir.
                if (Attribute.IsDefined(pi, typeof(PersonalBlog.DataAccess.PartialEntities.CryptoData)))
                {
                    dbContext.Entry(entity).Property(pi.Name).CurrentValue = _encryption.EncryptText(dbContext.Entry(entity).Property(pi.Name).CurrentValue.ToString());
                }
            }
        }
        return entity;
    }

    public virtual T DecryptEntityFields(T entity, PersonalBlogDbContext _dbcontext)
    {
        MetadataTypeAttribute[] metadataTypes = entity.GetType().GetCustomAttributes(true).OfType<MetadataTypeAttribute>().ToArray();
        foreach (MetadataTypeAttribute metadata in metadataTypes)
        {
            System.Reflection.PropertyInfo[] properties = metadata.MetadataClassType.GetProperties();
            //Metadata atanmış entity'nin tüm propertyleri tek tek alınır.
            foreach (System.Reflection.PropertyInfo pi in properties)
            {
                //Eğer ilgili property ait CryptoData flag'i var ise ilgili deger Decrypt edilir.
                if (Attribute.IsDefined(pi, typeof(PersonalBlog.DataAccess.PartialEntities.CryptoData)))
                {
                    _dbcontext.Entry(entity).Property(pi.Name).CurrentValue = _encryption.DecryptText(_dbcontext.Entry(entity).Property(pi.Name).CurrentValue.ToString());
                }
            }
        }
        return entity;
    }
    public virtual T UpdateEncryptedEntityFieldIfChange(T entity)
    {
        MetadataTypeAttribute[] metadataTypes = entity.GetType().GetCustomAttributes(true).OfType<MetadataTypeAttribute>().ToArray();
        foreach (MetadataTypeAttribute metadata in metadataTypes)
        {
            System.Reflection.PropertyInfo[] properties = metadata.MetadataClassType.GetProperties();
            //Metadata atanmış entity'nin tüm propertyleri tek tek alınır.
            foreach (System.Reflection.PropertyInfo pi in properties)
            {
                //Eğer ilgili property ait CryptoData flag'i var ise ilgili deger encrypt edilir.
                if (Attribute.IsDefined(pi, typeof(PersonalBlog.DataAccess.PartialEntities.CryptoData)))
                {
                    //Eğer şifreli property gerçekten değişmiş ise tekrardan şifrelenir. Önceki şifreli hali, yeni şifresiz hali şifrelenerek bakılır.
                    if (_context.Entry(entity).Property(pi.Name).OriginalValue.ToString() != _encryption.EncryptText(_context.Entry(entity).Property(pi.Name).CurrentValue.ToString()))
                    {
                        _context.Entry(entity).Property(pi.Name).CurrentValue = _encryption.EncryptText(_context.Entry(entity).Property(pi.Name).CurrentValue.ToString());
                    }
                    else
                    {
                        //Değişmediği için IsModified false atanır. Şifresiz hali geldiği için hiç güncellememek gerekir.
                        _context.Entry(entity).Property(pi.Name).IsModified = false;
                    }
                }
            }
        }
        return entity;
    }

}