using ContactService.Application.Services;
using ContactService.Domain.Entities;
using ContactService.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Infrastructure.Services {
    public class UserService : IUserService {
        private DBContext.AppDBContext _dbContext { get; }

        public UserService(DBContext.AppDBContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetAllAsync() {
            return await _dbContext.Users.AsNoTracking().ToListAsync();
        }

        public async Task<List<User>> GetAllWithDetailsAsync() {
            return await _dbContext.Users.AsNoTracking().Include(o => o.UserContactList).ToListAsync();
        }

        public async Task<User> GetAsync(int id) {
            return await _dbContext.Users.Include(o => o.UserContactList).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<User> CreateAsync(User user) {
            user.CreatedAt = user.UpdatedAt = DateTime.UtcNow;
            _dbContext.Users.Add(user);

            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateAsync(User user) {
            user.UpdatedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync(); // AppDbContext objesi scoped olarak inject edildigi icin controllerda cektigimiz user objesi hale mevcut context ile guncellenebilir.

            return user;
        }

        public async Task DeleteAsync(User user) {
            _dbContext.Users.Remove(user);
            if ((user.UserContactList?.Count ?? 0) > 0)
                _dbContext.UserContacts.RemoveRange(user.UserContactList);

            await _dbContext.SaveChangesAsync();
        }
    }
}
