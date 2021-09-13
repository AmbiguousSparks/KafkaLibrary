using KafkaLibrary.Consumer.Config;
using KafkaLibrary.Consumer.Interfaces;
using KafkaLibrary.Consumer.Services;
using KafkaLibrary.Producer.Config;
using KafkaLibrary.Producer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KafkaLibrary.Injetors
{
    public static class KafkaLibraryInjector
    {
        public static IServiceCollection AddKafkaLibrary(this IServiceCollection services, IConfiguration configuration, Assembly assembly) =>
            services
                .AddConsumers(configuration, assembly)
                .AddConsumerService()
                .AddProducers(configuration, assembly);

        public static IServiceCollection AddConsumers(this IServiceCollection services, IConfiguration configuration, Assembly assembly) =>
            services
                .AddSingleton(configuration.GetSection(nameof(ConsumerSettings)).Get<ConsumerSettings>())                
                .Scan(scan =>
                        scan
                            .FromAssemblies(assembly)
                            .AddClasses(classes =>
                                        classes
                                            .AssignableTo<IConsumer>())
                                            .AsMatchingInterface()
                                            .AsImplementedInterfaces());
        public static IServiceCollection AddConsumerService(this IServiceCollection services) =>
            services
                .AddHostedService<ConsumerService>();

        public static IServiceCollection AddProducers(this IServiceCollection services, IConfiguration configuration, Assembly assembly) =>
           services
               .AddSingleton(configuration.GetSection(nameof(ProducerSettings)).Get<ProducerSettings>())
               .Scan(scan =>
                       scan
                           .FromAssemblies(assembly)
                           .AddClasses(classes =>
                                       classes
                                           .AssignableTo(typeof(IProducer<>)))
                                           .AsMatchingInterface()
                                           .AsImplementedInterfaces());
    }
}
