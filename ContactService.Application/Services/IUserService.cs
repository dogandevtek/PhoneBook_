using ContactService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Application.Services {
    public interface IUserService : IRepository<User>{
        Task<List<User>> GetAllWithDetailsAsync();
    }
}
