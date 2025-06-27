using Microsoft.EntityFrameworkCore;

namespace ShowTime.DataAccess.Repositories;

public class BaseRepository<TEntity>(DbContext context) : IBaseRepository<TEntity>
    where TEntity : class
{
    protected readonly DbContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        try
        {
            return await _dbSet.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error while trying to retrieve all entities: {ex.Message}");
        }
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        try
        {
            return await _dbSet.FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error while trying to retrieve Entity by ID {id}", ex);
        }
    }

    public async Task CreateAsync(TEntity entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error while trying to create entity: {ex.Message}");
        }
    }

    public async Task UpdateAsync(int id, TEntity entity)
    {
        try
        {
            var entry = await _dbSet.FindAsync(id);
            if (entry != null)
            {
                _dbSet.Update(entity);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error while trying to update entity: {ex.Message}");
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error while trying to delete entity: {ex.Message}");
        }
    }
}