using System;

namespace CoreSB.Universal.Infrastructure
{
    public interface IBus
    {
        void Bind(string from, string to, string type = "direct");
        void ReceiveBind(Func<byte[], string> processMessage);
        void SendMessage(string message);
        string ProcessMessage(byte[] body);
    }
}
