using EventBus.Base.Abstraction;
using ReportGeneratorService.API.IntegrationEvents.Events;

namespace ReportGeneratorService.API.IntegrationEvents.EventHandlers {
    public class NewReportRequestedIntegrationEventHandler : IIntegrationEventHandler<NewReportRequestedIntegrationEvent> {
        

        public NewReportRequestedIntegrationEventHandler() {

        }

        public async Task Handle(NewReportRequestedIntegrationEvent e) {
            Console.WriteLine($"New status changed event received, Id : {e.Id}.");


        }
    }
}
