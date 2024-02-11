using ContactService.Domain.Entities;

namespace ContactService.Application.Services {
    public interface IRepository<T> where T : BaseEntity {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
