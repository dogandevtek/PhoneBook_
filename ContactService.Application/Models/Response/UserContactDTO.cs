using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Application.Models.Response {
    public class UserContactDTO {

        public int Id { get; set; }
        public int UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string E_mail { get; set; }
        public string Location { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
