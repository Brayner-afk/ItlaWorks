using Microsoft.EntityFrameworkCore;
using School.Infrastructure.Context;

namespace School.Infrastructure.Core;

public abstract class BaseRepository<TEntity> where TEntity : class
{
    protected readonly SchoolContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public BaseRepository(SchoolContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();
    public virtual async Task<TEntity?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
    public virtual async Task AddAsync(TEntity entity) { await _dbSet.AddAsync(entity); await _context.SaveChangesAsync(); }
    public virtual async Task UpdateAsync(TEntity entity) { _context.Entry(entity).State = EntityState.Modified; await _context.SaveChangesAsync(); }
}
