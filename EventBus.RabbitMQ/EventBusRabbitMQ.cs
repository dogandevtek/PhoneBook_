using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Base.Events;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace EventBus.RabbitMQ {
    public class EventBusRabbitMQ : BaseEventBus {
        private RabbitMQConnection rabbitMQConnection;
        private ConnectionFactory connectionFactory;
        private IModel consumerChannel;

        public EventBusRabbitMQ(EventBusConfig config, IServiceProvider serviceProvider) : base(config, serviceProvider) {
            if (config.Connection != null) connectionFactory = config.Connection as ConnectionFactory;
            if (connectionFactory == null) connectionFactory = new ConnectionFactory() { };

            rabbitMQConnection = new RabbitMQConnection(connectionFactory, config.ConnectionRetryCount);
            consumerChannel = CreateConsumerChannel();

            Console.WriteLine($"RabbitMQ connection {(rabbitMQConnection.IsConnected ? "succeed." : "failed!")}");

            SubsManager.OnEventRemoved += SubsManager_OnEventRemoved;
        }

        private void SubsManager_OnEventRemoved(object sender, string eventName) {
            eventName = ProcessEventName(eventName);

            if (!rabbitMQConnection.IsConnected)
                rabbitMQConnection.TryConnect();

            consumerChannel.QueueUnbind(queue: eventName,
                exchange: EventBusConfig.DefaultTopicName,
                routingKey: eventName);

            if (SubsManager.IsEmpty)
                consumerChannel.Close();
        }

        public override void Publish(IntegrationEvent @event) {
            if (!rabbitMQConnection.IsConnected)
                rabbitMQConnection.TryConnect();

            var policy = Policy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(EventBusConfig.ConnectionRetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) => {
                    // log
                });

            var eventName = ProcessEventName(@event.GetType().Name);

            consumerChannel.ExchangeDeclare(exchange: EventBusConfig.DefaultTopicName, type: ExchangeType.Direct); // Ensure exchange exists while publishing

            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            policy.Execute(() => {
                var properties = consumerChannel.CreateBasicProperties();
                properties.DeliveryMode = 2; // persistent

                consumerChannel.BasicPublish(
                    exchange: EventBusConfig.DefaultTopicName,
                    routingKey: eventName,
                    mandatory: true,
                    basicProperties: properties,
                    body: body);
            });
        }

        public override string getDefaultTopicName() { return EventBusConfig.DefaultTopicName; }

        public override void Subscribe<T, TH>() {
            var eventName = ProcessEventName(typeof(T).Name);

            if (!SubsManager.HasSubscriptionsForEvent(eventName)) {
                if (!rabbitMQConnection.IsConnected)
                    rabbitMQConnection.TryConnect();

                consumerChannel.QueueDeclare(queue: GetSubName(eventName), // Ensure queue exists while consuming
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                consumerChannel.QueueBind(queue: GetSubName(eventName),
                                  exchange: EventBusConfig.DefaultTopicName,
                                  routingKey: eventName);
            }

            SubsManager.AddSubscription<T, TH>();
            StartBasicConsume(eventName);
        }

        public override void UnSubscribe<T, TH>() {
            SubsManager.RemoveSubscription<T, TH>();
        }


        private IModel CreateConsumerChannel() {
            if (!rabbitMQConnection.IsConnected)
                rabbitMQConnection.TryConnect();

            var channel = rabbitMQConnection.CreateModel();
            channel.ExchangeDeclare(exchange: EventBusConfig.DefaultTopicName, type: ExchangeType.Direct);

            return channel;
        }

        private void StartBasicConsume(string eventName) {
            if (consumerChannel != null) {
                var consumer = new EventingBasicConsumer(consumerChannel);

                consumer.Received += Consumer_Received;

                consumerChannel.BasicConsume(
                    queue: GetSubName(eventName),
                    autoAck: false,
                    consumer: consumer);
            }
        }

        private async void Consumer_Received(object sender, BasicDeliverEventArgs e) {
            var eventName = e.RoutingKey;
            eventName = ProcessEventName(eventName);
            var message = Encoding.UTF8.GetString(e.Body.Span);

            try {
                await ProcessEvent(eventName, message);
            } catch (Exception ex) {
                int a = 0;
                a++;
                // logging
            }

            consumerChannel.BasicAck(e.DeliveryTag, multiple: false);
        }

    }
}
