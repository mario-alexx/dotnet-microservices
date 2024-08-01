using System;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;
using RabbitMQ.Client;

namespace PlatformService.AsynDataServices
{
    /// <summary>
    /// Implementation of the IMessageBusClient interface for publishing messages to a message bus using RabbitMQ.
    /// </summary>
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _config;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBusClient"/> class.
        /// </summary>
        /// <param name="config">The configuration settings.</param>
        public MessageBusClient(IConfiguration config)
        {
            _config = config;

            // Initialize RabbitMQ connection  
            var factory = new ConnectionFactory() { 
                HostName = _config["RabbitMQHost"], 
                Port = int.Parse(_config["RabbitMQPort"]) };
        
            try
            {
               _connection = factory.CreateConnection(); 
               _channel = _connection.CreateModel();

               _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                Console.WriteLine("--> Connected to MessageBus");
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"--> Could not connect to the Message Bus: { ex.Message }");
            }
        }

        /// <summary>
        /// Publishes a new platform event to the message bus.
        /// </summary>
        /// <param name="platformPublishedDto">The platform published DTO containing event data.</param>
        public void PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
        {
            var message = JsonSerializer.Serialize(platformPublishedDto);

            if(_connection.IsOpen) 
            {
                Console.WriteLine("--> RabbitMQ Connection Open, sending message...");
                SendMessage(message);
            }
            else 
            {                
                Console.WriteLine("--> RabbitMQ Connection closed, not sending ...");
            }
        }
        
        /// <summary>
        /// Sends a message to the RabbitMQ message bus.
        /// </summary>
        /// <param name="message">The message to send.</param>
        private void SendMessage(string message) 
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(
                exchange: "trigger", routingKey: "", basicProperties: null, body: body
            );

            System.Console.WriteLine($"--> We have sent { message }");
        }

        /// <summary>
        /// Disposes the RabbitMQ connection and channel.
        /// </summary>
        public void Dispose()
        {
            Console.WriteLine("MessaggeBus Disposed");
            if(_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }

        /// <summary>
        /// Handles RabbitMQ connection shutdown events.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The shutdown event arguments.</param>
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) 
        {
            Console.WriteLine("--> RabbitMQ Connection Shutdown");
        }
    }
}