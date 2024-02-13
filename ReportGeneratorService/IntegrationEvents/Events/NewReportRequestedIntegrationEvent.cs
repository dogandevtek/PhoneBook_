using EventBus.Base.Events;

namespace ReportGeneratorService.API.IntegrationEvents.Events {
    public class NewReportRequestedIntegrationEvent : IntegrationEvent {
        public NewReportRequestedIntegrationEvent(int reportId) {
            ReportId = reportId;
        }

        public int ReportId { get; set; }
    }
}
