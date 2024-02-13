using EventBus.Base.Events;

namespace ReportService.API.IntegrationEvents.Events {
    public class NewReportRequestedIntegrationEvent : IntegrationEvent {
        public NewReportRequestedIntegrationEvent(int reportId) {
            ReportId = reportId;
        }

        public int ReportId { get; set; }
    }
}
