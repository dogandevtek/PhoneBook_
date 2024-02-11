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
    public class UserContactService : IUserContactService {
        private DBContext.AppDBContext _dbContext { get; }

        public UserContactService(DBContext.AppDBContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<List<UserContact>> GetAllAsync() {
            throw new NotSupportedException();
        }

        public async Task<List<UserContact>> GetAllByUserIdAsync(int userId) {
            return await _dbContext.UserContacts.AsNoTracking().Where(o => o.UserId == userId).ToListAsync();
        }

        public async Task<UserContact> GetAsync(int id) {
            return await _dbContext.UserContacts.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<UserContact> CreateAsync(UserContact userContact) {
            userContact.CreatedAt = userContact.UpdatedAt = DateTime.UtcNow;
            _dbContext.UserContacts.Add(userContact);

            await _dbContext.SaveChangesAsync();

            return userContact;
        }

        public async Task<UserContact> UpdateAsync(UserContact userContact) {
            userContact.UpdatedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync(); // AppDbContext objesi scoped olarak inject edildigi icin controllerda cektigimiz user objesi hale mevcut context ile guncellenebilir.

            return userContact;
        }

        public async Task DeleteAsync(UserContact userContact) {
            _dbContext.UserContacts.Remove(userContact);
            await _dbContext.SaveChangesAsync();
        }
    }
}
