using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.Repositories.Interfaces;

namespace ShowTime.DataAccess.Repositories;

public class BaseRepository<TEntity>(ShowTimeDbContext context) : IRepository<TEntity>
    where TEntity : class
{
    protected readonly ShowTimeDbContext Context = context;

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        try
        {
            return await Context.Set<TEntity>().ToListAsync();
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
            return await Context.Set<TEntity>().FindAsync(id);
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
            Context.Set<TEntity>().Add(entity);
            await Context.SaveChangesAsync();
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
            Context.Set<TEntity>().Update(entity);
            await Context.SaveChangesAsync();
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
            var entity = await Context.Set<TEntity>().FindAsync(id);
            if (entity != null)
            {
                Context.Set<TEntity>().Remove(entity);
                await Context.SaveChangesAsync();
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