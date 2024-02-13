using ContactService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Application.Models.Response {
    public class UserEDTO {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Company { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<UserContactDTO> UserContactList { get; set; }
    }
}
