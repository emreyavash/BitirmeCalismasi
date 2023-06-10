using AutoMapper;
using ETicaret.Users.Entities;
using ETicaret.Users.Entities.DTOs;
using ETicaret.Users.Repositories.Interface;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Core;
using EventBusRabbitMQ.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ETicaret.Users.Consumers
{
    public class EventBusUserCreateConsumer
    {
        private readonly IRabbitMQPersistentConnection _consumer;
        private readonly IUserRepository _userRepository;

        public EventBusUserCreateConsumer(IRabbitMQPersistentConnection consumer, IUserRepository userRepository)
        {
            _consumer = consumer;
            _userRepository = userRepository;
        }

        public void Consume()
        {
            if (!_consumer.IsConnected)
            {
                _consumer.TryConnect();
            }

            var channel = _consumer.CreateModel();
            channel.QueueDeclare(queue:EventBusConstants.UserCreateQueue,durable:false,exclusive:false,autoDelete:false,arguments:null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += ReceivedEvent;
            channel.BasicConsume(queue: EventBusConstants.UserCreateQueue, autoAck: true, consumer: consumer);


        }

        private async void ReceivedEvent(object sender,BasicDeliverEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Body.Span);
            var @event = JsonConvert.DeserializeObject<UserCreateEvent>(message);

            if(e.RoutingKey == EventBusConstants.UserCreateQueue)
            {
                var user = new UserForRegisterDTO();
                user.FirstName = @event.FirstName;
                user.LastName = @event.LastName;
                user.Email = @event.Email;
                user.Password = @event.Password;

               await _userRepository.Register(user,@event.Password);
            }
        }
        public void Disconnect()
        {
            _consumer.Dispose();
        }
    }
}
