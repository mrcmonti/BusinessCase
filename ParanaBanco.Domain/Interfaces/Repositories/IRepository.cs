using ParanaBanco.Domain.Entities.SeedWork;

namespace ParanaBanco.Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<T> SelectAsync(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(int offset, int limit);
        Task<int> CountAsync();
        Task<T> InsertAsync(T entity);

        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
