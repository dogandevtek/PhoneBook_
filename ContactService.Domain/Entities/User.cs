using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Domain.Entities {
    public class User : BaseEntity {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Company { get; set; }

        public virtual ICollection<UserContact> UserContactList { get; set; }
    }
}
