using System.Threading;
using System.Threading.Tasks;

namespace KafkaLibrary.Consumer.Delegates
{
    public delegate Task ConsumeHandler<T>(T data, CancellationToken cancellationToken = default);
}
