using EventBus.Base.Events;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ReportService.Domain.Enum;

namespace ReportService.API.IntegrationEvents.Events {
    public class ReportStatusChangedIntegrationEvent : IntegrationEvent {

        public ReportStatusChangedIntegrationEvent(int reportId, ReportStatuses status, string path) {
            ReportId = reportId;
            Status = status;
            ResultPath = path;
        }

        public int ReportId { get; set; }
        public ReportStatuses Status { get; set; }
        public string ResultPath { get; set; }

    }
}
