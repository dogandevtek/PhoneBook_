using ReportService.Domain.Entities;

namespace ReportService.Application.Services {
    public interface IRepository<T> where T : BaseEntity {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
