
using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.RabbitMQ;
using System;

namespace EventBus.Factory
{
    public static class EventBusFactory
    {
        public static IEventBus Create(EventBusConfig config, IServiceProvider serviceProvider)
        {
            return new EventBusRabbitMQ(config, serviceProvider);
            /*switch (config.EventBusType) {
                case EventBusType.RabbitMQ:
                    break;
                case EventBusType.AzureServiceBus:
                    break;
            }*/
        }
    }
}
