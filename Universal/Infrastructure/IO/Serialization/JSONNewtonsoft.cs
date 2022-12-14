
namespace CoreSB.Infrastructure.IO.Serialization
{

    using CoreSB.Universal;
    using Newtonsoft.Json;

    public class JSONNewtonsoft : ISerialization
    {
        public object DeSerialize(string input)
        {
            return JsonConvert.DeserializeObject(input);
        }

        public string Serialize(object input)
        {
            return JsonConvert.SerializeObject(input);
        }

        public T DeSerialize<T>(string input)
        {
            return JsonConvert.DeserializeObject<T>(input);
        }

        public string Serialize<T>(T input)
        {
            return JsonConvert.SerializeObject(input);
        }
    }
}
