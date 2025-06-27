namespace ShowTime.DataAccess.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(int id);
    Task CreateAsync(TEntity entity);
    Task UpdateAsync(int id, TEntity entity);
    Task DeleteAsync(int id);
}