using Confluent.Kafka;
using KafkaLibrary.Producer.Config;
using KafkaLibrary.Producer.Interfaces;
using KafkaLibrary.Producer.Serializer;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaLibrary.Producer.Abstracts
{
    public abstract class ProducerBase<T> : IProducer<T>, IDisposable
    {
        public abstract string Topics { get; }

        private readonly IProducer<string, T> _producer;
        public ProducerBase(ProducerSettings settings)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = settings.BootstrapServers,
                MessageTimeoutMs = settings.Timeout
            };

            _producer = new ProducerBuilder<string, T>(config).SetValueSerializer(new JsonSerializer<T>()).Build();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ProduceException{String, T}"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>        
        public async Task ProduceAsync(string key, T data, CancellationToken cancellationToken = default)
        {
            using (_producer)
            {
                var message = new Message<string, T>()
                {
                    Key = key,
                    Value = data
                };
                await _producer.ProduceAsync(Topics, message, cancellationToken);
            }
        }

        public void Dispose()
        {            
            _producer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
