//Interface para definir a assinatura dos métodos que o GenericService deve implementar
namespace Domain.Interfaces.Services
{
    public interface IGenericService<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}