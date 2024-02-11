using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using ReportService.API.IntegrationEvents.EventHandlers;
using ReportService.API.IntegrationEvents.Events;

namespace ReportService.API.Registration {
    public static class IntegrationEventsRegistration {
        public static IServiceCollection ConfigureIntegrationEvents(this IServiceCollection services) {

            services.AddSingleton<IEventBus>(sp => {
                var config = new EventBusConfig() {
                    ConnectionRetryCount = 5,
                    EventNameSuffix = "IntegrationEvent",
                    SubscriberClientAppName = $"ReportServiceApi",
                    DefaultTopicName = $"ReportServiceApi_Exchange",
                    EventBusType = EventBusType.RabbitMQ
                };

                return EventBusFactory.Create(config, sp);
            });


            services.AddTransient<ReportStatusChangedIntegrationEventHandler>();

            return services;
        }

        public static IApplicationBuilder RegisterIntegrationEvents(this IApplicationBuilder app) {
            IEventBus eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus?.Subscribe<ReportStatusChangedIntegrationEvent, ReportStatusChangedIntegrationEventHandler>();

            return app;
        }
    }
}
