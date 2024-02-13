using EventBus.Base.Abstraction;
using ReportGeneratorService.API.IntegrationEvents.Events;
using ReportGeneratorService.Services;

namespace ReportGeneratorService.API.IntegrationEvents.EventHandlers {
    public class NewReportRequestedIntegrationEventHandler : IIntegrationEventHandler<NewReportRequestedIntegrationEvent> {
        IReportGenerator _reportGenerator;
        IEventBus _eventBus;

        public NewReportRequestedIntegrationEventHandler(IReportGenerator reportGenerator, IEventBus eventBus) {
            _reportGenerator = reportGenerator;
            _eventBus = eventBus;
        }

        public async Task Handle(NewReportRequestedIntegrationEvent e) {
            Console.WriteLine($"New report requested event received, Id : {e.Id}.");

            _eventBus.Publish(new ReportStatusChangedIntegrationEvent(e.ReportId, Domain.Enum.ReportStatuses.Preparing, ""));

            await _reportGenerator.generate(e.ReportId);
        }
    }
}
