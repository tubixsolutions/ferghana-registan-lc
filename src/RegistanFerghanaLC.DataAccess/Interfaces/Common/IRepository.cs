using RegistanFerghanaLC.Domain.Common;
using System.Linq.Expressions;

namespace RegistanFerghanaLC.DataAccess.Interfaces.Common;
public interface IRepository<T> where T : BaseEntity
{
    public Task<T?> FindByIdAsync(int id);

    public Task<T?> FirstOrDefault(Expression<Func<T, bool>> expression);

    public void Add(T entity);

    public void Update(int id, T entity);

    public void Delete(int id);
}
