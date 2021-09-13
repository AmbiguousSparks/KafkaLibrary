using Confluent.Kafka;
using Newtonsoft.Json;
using System.Text;

namespace KafkaLibrary.Producer.Serializer
{
    public class JsonSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            var json = JsonConvert.SerializeObject(data);
            return Encoding.Default.GetBytes(json);
        }
    }
}
