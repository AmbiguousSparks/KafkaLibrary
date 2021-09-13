using System.Threading;
using System.Threading.Tasks;

namespace KafkaLibrary.Producer.Interfaces
{
    public interface IProducer<T>
    {
        Task ProduceAsync(string key, T data, CancellationToken cancellationToken = default);
        string Topics { get; }
    }
}
