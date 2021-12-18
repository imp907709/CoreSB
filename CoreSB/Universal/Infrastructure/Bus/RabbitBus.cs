
namespace CoreSB.Universal.Infrastructure.Bus
{
    using System;
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    public class RabbitBus
    {
        public static bool received = false;
        public static bool receiveing = false;

        IConnection _connection;
        IModel _channelRecieve;
        IModel _channelEmit;
        ConnectionFactory _factory;

        public const string defaultExchangeName = "default_exchange_1";
        public const string defaultQueueName = "default_queue_1";
        public const string defaultRoutingKey = "default_routing_1";

        public IConnection CreateConnection(string _hostname)
        {
            _factory = new ConnectionFactory() { HostName = _hostname };
            _connection = _factory.CreateConnection();
            return _connection;
        }

        public IModel CreateChannelEmit()
        {
            _channelEmit = _connection.CreateModel();
            return _channelEmit;
        }
        public IModel CreateChannelReceive()
        {
            _channelRecieve = _connection.CreateModel();
            return _channelRecieve;
        }
        public void CloseChannel()
        {
            _channelRecieve.Close();

        }

        public void ExchangeDelete(string exchange = defaultExchangeName, bool ifUnused = true)
        {
            _channelRecieve.ExchangeDelete(exchange, ifUnused);
        }
        public void ExchangeDeclare(string exchange = defaultExchangeName, string type = "direct", bool durable = false, bool autoDelete = false)
        {

            _channelRecieve.ExchangeDeclare(exchange, type, durable, autoDelete);
        }
        public void ExchangeRecreate(string exchange = defaultExchangeName, string type = "direct", bool durable = false, bool autoDelete = false, bool ifUnused = true)
        {
            ExchangeDelete(exchange, ifUnused);
            ExchangeDeclare(exchange, type, durable, autoDelete);
        }

        public string QueueDeclare(string queueName = defaultQueueName, bool durable = false, bool exclusive = true, bool auodelete = true)
        {
            return _channelRecieve.QueueDeclare(queueName, durable, exclusive, auodelete).QueueName;
        }
        public void QueueDelete(string queueName = defaultQueueName, bool ifUnused = false, bool ifEmpty = false)
        {
            _channelRecieve.QueueDelete(queueName, ifUnused, ifEmpty);
        }
        public string QueueRecreate(string queueName = defaultQueueName, bool durable = false, bool exclusive = true, bool auodelete = true, bool ifUnused = false, bool ifEmpty = false)
        {
            QueueDelete(queueName, ifUnused, ifEmpty);
            return QueueDeclare(queueName, durable, exclusive, auodelete);
        }

        public void QueueBind(string queueName = defaultQueueName, string exchangeName = defaultExchangeName, string routingKey = defaultRoutingKey)
        {
            _channelRecieve.QueueBind(queueName, exchangeName, routingKey);
        }

        public void MessagePublish(string msg, string exchangeName = defaultExchangeName, string routingKey = defaultRoutingKey, IBasicProperties props = null)
        {
            byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes(msg);
            _channelRecieve.BasicPublish(exchangeName, routingKey, props, messageBodyBytes);
        }

        public void RegisterConsume(Func<byte[], string> readBody, string queueName = defaultQueueName)
        {
            _channelRecieve = _connection.CreateModel();
            var consumer = register(_channelRecieve, readBody);
            String consumerTag = _channelRecieve.BasicConsume(queueName, false, consumer);
        }
        public static EventingBasicConsumer register(IModel channel, Func<byte[], string> readBody)
        {
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                readBody(ea.Body.ToArray());
                channel.BasicAck(ea.DeliveryTag, false);
                received = true;
            };
            return consumer;
        }
        public static string parseresponse(byte[] body)
        {
            var message = Encoding.UTF8.GetString(body);
            return message;
        }

        public delegate string byteBody(byte[] body);

        public void defaultReceive(
            string hostname,
            Func<byte[], string> readBody,
            string exchange = defaultExchangeName,
            string type = "direct",
            string queueName = defaultQueueName,
            string routingKey = defaultRoutingKey,

            bool durable = false,
            bool autoDelete = false,
            bool durableQueue = false,
            bool exclusive = true,
            bool auodelete = true,
            bool ifUnused = false,
            bool ifEmpty = false)
        {
            CreateConnection(hostname);
            CreateChannelReceive();
            ExchangeRecreate(exchange, type, durable, autoDelete, ifUnused);
            QueueRecreate(queueName, durable, exclusive, autoDelete, ifUnused, ifEmpty);
            QueueBind(queueName, exchange, routingKey);
            RegisterConsume(readBody, queueName);
            receiveing = true;
        }

        public void defaultEmit(string hostname, string message, string exchange = defaultExchangeName, string routingKey = defaultRoutingKey)
        {
            CreateConnection(hostname);
            CreateChannelReceive();
            MessagePublish(message, exchange, routingKey);
        }


        public IConnection GetConnection()
        {
            return _connection;
        }
        public IModel GetCahnnel()
        {
            return _channelRecieve;
        }

        public void Dispose()
        {
            _channelRecieve?.Close();
            _connection?.Close();
            _channelRecieve?.Close();

            _channelRecieve?.Dispose();
            _connection?.Dispose();
            _channelRecieve?.Dispose();
        }



        public static EventingBasicConsumer registerToAsync(IModel channel)
        {
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                await parseresponseConsole(model, ea, channel);
                //channel.BasicAck(ea.DeliveryTag, false);
                //received = true;               
            };
            //consumer.Received += new EventHandler<BasicDeliverEventArgs>(async (s, e) => await parseresponseTest(s,e,channel));
            return consumer;
        }

        public static async Task<int> parseresponseConsole(object model, BasicDeliverEventArgs ea, IModel channel)
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            var rnd = new System.Random();
            var processTime = rnd.Next(200, (message.Length + 4) * 1000);
            Console.WriteLine($"processing {processTime} secs");
            await Task.Delay(rnd.Next(200, processTime));
            Console.WriteLine(" [x] Received {0}", message);

            channel.BasicAck(ea.DeliveryTag, false);
            received = true;
            return 1;
        }

    }
}
