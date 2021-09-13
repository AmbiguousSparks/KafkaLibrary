using KafkaLibrary.Consumer.Interfaces;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaLibrary.Consumer.Services
{
    internal class ConsumerService : BackgroundService
    {
        private readonly IEnumerable<IConsumer> _consumers;

        public ConsumerService(IEnumerable<IConsumer> consumers)
        {
            _consumers = consumers;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var tasks = _consumers.Select(c => c.ConsumeAsync(stoppingToken));
            await Task.WhenAll(tasks);
        }
    }
}
