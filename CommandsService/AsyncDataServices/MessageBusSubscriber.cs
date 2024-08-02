using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommandsService.EventProcessing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CommandsService.AsyncDataServices 
{
    /// <summary>
    /// Background service for subscribing to the message bus and processing messages.
    /// </summary>
    public class MessageBusSubscriber : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IEventProcessor _eventProcessor;
        private IConnection _connection;
        private IModel _channel;
        private string _queueName;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBusSubscriber"/> class.
        /// </summary>
        /// <param name="configuration">The configuration settings.</param>
        /// <param name="eventProcessor">The event processor for handling incoming messages.</param>
        public MessageBusSubscriber(IConfiguration configuration, IEventProcessor eventProcessor)
        {
            _configuration = configuration;
            _eventProcessor = eventProcessor;

            InitializeRabbitMQ();
        }

        /// <summary>
        /// Initializes the RabbitMQ connection and channel.
        /// </summary>
        private void InitializeRabbitMQ() 
        {
            var factory = new ConnectionFactory() { HostName = _configuration["RabbitMQHost"], Port = int.Parse(_configuration["RabbitMQPort"])};
        
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
            _queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue:_queueName, exchange: "trigger", routingKey: "");

            Console.WriteLine("--> Listening on the Message Bus...");

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        /// <summary>
        /// Executes the background service, listening for messages on the message bus.
        /// </summary>
        /// <param name="stoppingToken">Token to signal when the task should stop.</param>
        /// <returns>A task that represents the background service execution.</returns>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (ModuleHandle, ea) => 
            {
                Console.WriteLine("--> Event Received!");
                var body = ea.Body;
                var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

                _eventProcessor.ProcessEvent(notificationMessage);
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
            
            return Task.CompletedTask;
        }

        /// <summary>
        /// Handles the RabbitMQ connection shutdown event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The shutdown event arguments.</param>
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> Connection Shutdown");
        }

        /// <summary>
        /// Disposes the RabbitMQ connection and channel.
        /// </summary>
        public override void Dispose()
        {
            if(_channel.IsOpen) 
            {
                _channel.Close();
                _connection.Close();
            }
        }
    }
}