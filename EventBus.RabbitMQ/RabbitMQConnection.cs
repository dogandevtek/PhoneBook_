using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.RabbitMQ
{
    public class RabbitMQConnection : IDisposable
    {

        public bool IsConnected => connection != null && connection.IsOpen;

        private IConnection connection;
        private IConnectionFactory ConnectionFactory;
        private int RetryCount;

        private object lock_object = new object();

        private bool _disposed = false;

        public RabbitMQConnection(IConnectionFactory connectionFactory, int retryCount) {
            ConnectionFactory = connectionFactory;
            RetryCount = retryCount;
        }

        public IModel CreateModel() {
            return connection.CreateModel();
        }

        public bool TryConnect() {
            lock (lock_object) {
                var policy = Policy.Handle<SocketException>().Or<BrokerUnreachableException>().WaitAndRetry(RetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) => { });

                policy.Execute(() => {
                    connection = ConnectionFactory.CreateConnection();
                });
            }

            if (IsConnected) {
                connection.ConnectionShutdown += (ss, ee) => { if(!_disposed) TryConnect(); };
                connection.CallbackException += (ss, ee) => { if (!_disposed) TryConnect(); };
                connection.ConnectionBlocked += (ss, ee) => { if (!_disposed) TryConnect(); };
            }

            return IsConnected;
        }

        public void Dispose() {
            _disposed = true;
            throw new NotImplementedException();
        }
    }
}
