using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Application.Models.Request {
    public class UserUDTO {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Company { get; set; }
    }
}
