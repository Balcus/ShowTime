using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.Repositories.Interfaces;

namespace ShowTime.DataAccess.Repositories;

public class BaseRepository<TEntity>(DbContext context) : IRepository<TEntity>
    where TEntity : class
{
    protected readonly DbContext _context = context;
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
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

    public virtual async Task<TEntity?> GetByIdAsync(int id)
    {
        try
        {
            return await _dbSet.FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error while trying to retrieve Entity by ID: {ex.Message}");
        }
    }

    public virtual async Task CreateAsync(TEntity entity)
    {
        try
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            throw new Exception($"Database error while adding new {typeof(TEntity).Name}: {e.InnerException?.Message ?? e.Message}");
        }
        catch (ValidationException e)
        {
            throw new Exception($"Validation failed while adding {typeof(TEntity).Name}: {e.Message}");
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to add new {typeof(TEntity).Name}: {e.Message}");
        }
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        try
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            throw new Exception($"Database error while updating {typeof(TEntity).Name}: {e.InnerException?.Message ?? e.Message}");
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to update {typeof(TEntity).Name}: {e.Message}");
        }
    }

    public virtual async Task DeleteAsync(int id)
    {
        try
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        catch (DbUpdateException e)
        {
            throw new Exception($"Database error while deleting {typeof(TEntity).Name} with ID {id}: {e.InnerException?.Message ?? e.Message}");
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to delete {typeof(TEntity).Name} with ID {id}: {e.Message}");
        }
    }
}