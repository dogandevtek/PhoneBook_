using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;

using Microsoft.Extensions.DependencyInjection;

using RabbitMQ.Client;

using ReportGeneratorService.API.IntegrationEvents.EventHandlers;
using ReportGeneratorService.API.IntegrationEvents.Events;
using ReportGeneratorService.Services;

namespace ReportGeneratorService {
    public class Program {
        static async Task Main(string[] args) {

            var serviceProvider = new ServiceCollection()
                                   .AddSingleton<IEventBus>(sp => {
                                       return EventBusFactory.Create(new EventBusConfig() {
                                           ConnectionRetryCount = 5,
                                           EventNameSuffix = "IntegrationEvent",
                                           SubscriberClientAppName = $"ReportGeneratorService",
                                           DefaultTopicName = $"ReportService_Exchange", //Exchange
                                           EventBusType = EventBusType.RabbitMQ,
                                           Connection = new ConnectionFactory {
#if DEBUG
                                               HostName = "localhost",
#else
                                               HostName = "rabbitmq-container",
#endif
                                               Port = 5672,
                                               //UserName = "guest",
                                               //Password = "guest",
                                           }
                                       }, sp);
                                   })
                                   .AddTransient<IReportGenerator, ReportGenerator>()
                                   .AddTransient<NewReportRequestedIntegrationEventHandler>()
                                   .BuildServiceProvider();

            var eventBus = serviceProvider.GetRequiredService<IEventBus>();

            eventBus.Subscribe<NewReportRequestedIntegrationEvent, NewReportRequestedIntegrationEventHandler>();

            await Task.Delay(Timeout.Infinite);
        }
    }
}