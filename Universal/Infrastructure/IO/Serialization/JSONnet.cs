
namespace CoreSB.Universal.Infrastructure.IO.Serialization
{
    using System.Text.Json;
    using CoreSB.Universal;

    public class JSONnet : ISerialization
    {
        public object DeSerialize(string input)
        {
            return JsonSerializer.Deserialize<object>(input);
        }

        public T DeSerialize<T>(string input)
        {
            return JsonSerializer.Deserialize<T>(input);
        }

        public string Serialize(object input)
        {
            return JsonSerializer.Serialize(input);
        }

        public string Serialize<T>(T item)
        {
            return JsonSerializer.Serialize<T>(item);
        }
    }
}
