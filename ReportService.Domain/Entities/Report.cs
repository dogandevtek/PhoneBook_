using ReportService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Entities {
    public class Report : BaseEntity {        
        public ReportStatuses Status { get; set; }
        public string Path { get; set; }
    }
}
