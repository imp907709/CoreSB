
namespace CoreSB.Universal.Infrastructure.Bus
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BusWrapper : IBus
    {
        RabbitBus rabbit;

        string _hostname => "localhost";

        string _from = "from";
        string _to = "to";
        string _type = "direct";

        string _exchangeReceive => $"exchange_from_{_from}_to_{_to}";
        string _queueNameReceive => $"queue_from_{_from}_to_{_to}";
        string _routingReceive => $"routing_from_{_from}_to_{_to}";

        string _exchangeSend => $"exchange_to_{_from}_from_{_to}";
        string _routingKey => $"routing_to_{_from}_from_{_to}";


        public BusWrapper()
        {
            rabbit = new RabbitBus();
        }
        public BusWrapper(string from, string to, string type = "direct")
        {
            rabbit = new RabbitBus();
            _from = from;
            _to = to;
            _type = type;
        }

        public void Bind(string from, string to, string type = "direct")
        {
            _from = from;
            _to = to;
            _type = type;
        }

        public void ReceiveBind(Func<byte[], string> processMessage)
        {
            rabbit.defaultReceive(_hostname, processMessage, _exchangeReceive, _type, _queueNameReceive, _routingReceive);
        }
        public void SendMessage(string message)
        {
            rabbit.defaultEmit(_hostname, message, _exchangeSend, _routingKey);
        }
        public string ProcessMessage(byte[] body)
        {
            var message = Encoding.UTF8.GetString(body);
            return message;
        }
    }
}
