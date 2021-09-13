using KafkaLibrary.Consumer.Interfaces;
using Confluent.Kafka;
using KafkaLibrary.Consumer.Config;
using KafkaLibrary.Consumer.Deserializer;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace KafkaLibrary.Consumer.Abstracts
{
    public abstract class ConsumerBase<T> : IConsumer, IDisposable
    {
        public abstract string[] Topics { get; }

        public abstract string GroupId { get; }

        private readonly IConsumer<string, T> _consumer;

        public ConsumerBase(ConsumerSettings settings)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = settings.BootstrapServers,
                GroupId = GroupId,
                AllowAutoCreateTopics = true
            };
            _consumer = new ConsumerBuilder<string, T>(config).SetValueDeserializer(new JsonDeserializer<T>()).Build();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ConsumeException"></exception>
        /// <returns></returns>
        public async Task ConsumeAsync(CancellationToken cancellationToken = default)
        {
            using (_consumer)
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        _consumer.Subscribe(Topics);
                        var result = await Task.Run(() => _consumer.Consume(cancellationToken), cancellationToken).ConfigureAwait(false);
                        if (result is not null)
                        {
                            await HandleConsumeAsync(result, cancellationToken).ConfigureAwait(false);
                            _consumer.Commit(result);
                        }
                    }
                    catch (OperationCanceledException)
                    {
                    }
                }
            }
        }

        protected abstract Task HandleConsumeAsync(ConsumeResult<string, T> result, CancellationToken cancellationToken = default);

        public void Dispose()
        {
            _consumer.Close();
            _consumer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
