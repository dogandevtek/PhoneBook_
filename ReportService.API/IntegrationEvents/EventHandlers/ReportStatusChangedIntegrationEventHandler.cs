using AutoMapper;
using EventBus.Base.Abstraction;
using ReportService.API.IntegrationEvents.Events;
using ReportService.Application.Services;

namespace ReportService.API.IntegrationEvents.EventHandlers {
    public class ReportStatusChangedIntegrationEventHandler : IIntegrationEventHandler<ReportStatusChangedIntegrationEvent> {
        private readonly IReportService _reportService;

        public ReportStatusChangedIntegrationEventHandler(IReportService reportService) {
            _reportService = reportService;
        }

        public async Task Handle(ReportStatusChangedIntegrationEvent e) {
            Console.WriteLine($"New status changed event received, Id : {e.Id}, status : {e.Status}.");

            var report = await _reportService.GetAsync(e.ReportId);   
            if(report != null) {
                report.Status = e.Status;
                report.Path = e.ResultPath;

                await _reportService.UpdateAsync(report);
            }
        }
    }
}
