using ReportService.Domain.Enum;

namespace ReportService.Application.Models {
    public class ReportDTO {
        public int Id { get; set; }
        public ReportStatuses Status { get; set; }
        public string Path { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
