using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Application.Models.Request {
    public class UserContactUDTO {

        public int UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string E_mail { get; set; }
        public string Location { get; set; }
    }
}
