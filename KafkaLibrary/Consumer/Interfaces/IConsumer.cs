using System.Threading;
using System.Threading.Tasks;

namespace KafkaLibrary.Consumer.Interfaces
{
    public interface IConsumer
    {
        string[] Topics { get; }
        string GroupId { get; }
        Task ConsumeAsync(CancellationToken cancellationToken = default);
    }
}
