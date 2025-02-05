namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly Dbset<T> _dbSet;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbset = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task AddAsync(T entity) 
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            await _dbSet.UpdateAsync(entity);
            await _context.SaveChangesAsync();
        } 
        public async Task DeleteAsync(T entity)
        {
            await _dbSet.DeleteAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}