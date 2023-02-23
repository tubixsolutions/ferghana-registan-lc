namespace RegistanFerghanaLC.DataAccess.Interfaces.Common;
public interface IUnitOfWork
{
    public Task<int> SaveChangesAsync();
}
