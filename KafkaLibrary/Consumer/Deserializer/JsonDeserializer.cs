using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Text;

namespace KafkaLibrary.Consumer.Deserializer
{
    public class JsonDeserializer<T> : IDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            var json = Encoding.Default.GetString(data);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
