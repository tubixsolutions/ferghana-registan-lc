using Microsoft.EntityFrameworkCore;
using RegistanFerghanaLC.DataAccess.DbContexts;
using RegistanFerghanaLC.DataAccess.Interfaces.Common;
using RegistanFerghanaLC.Domain.Common;
using System.Linq.Expressions;

namespace RegistanFerghanaLC.DataAccess.Repositories.Common;
public class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public BaseRepository(AppDbContext appDbContext)
    {
        this._dbContext = appDbContext;
        this._dbSet = appDbContext.Set<T>();
    }
    public virtual void Add(T entity)
        => _dbSet.Add(entity);

    public virtual void Delete(int id)
    {
        var entity = _dbSet.Find(id);
        if(entity is not null)
            _dbSet.Remove(entity);
    }

    public virtual async Task<T?> FindByIdAsync(int id)
        => await _dbSet.FindAsync(id);

    public virtual async Task<T?> FirstOrDefault(Expression<Func<T, bool>> expression)
        => await _dbSet.FirstOrDefaultAsync(expression);

    public virtual void Update(int id, T entity)
    {
        entity.Id = id;
        _dbSet.Update(entity);
    }
}
