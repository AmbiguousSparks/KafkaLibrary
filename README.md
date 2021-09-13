# KafkaLibrary
A kafka library to help you build producer and consumers

# Configuration
In order to use it's depency injector, you need to add some configurations to your configuration file eg:
![image](https://user-images.githubusercontent.com/45923706/133131964-8d154045-ba0e-4928-aaf4-c6f3da03433b.png)

and then you can call the AddKafkaLibrary extension passing the assembly of the producers and consumers
![image](https://user-images.githubusercontent.com/45923706/133132042-bdd85e12-d67e-48e9-b5d9-9c6cbd005059.png)

you can call each one of the services apart

![image](https://user-images.githubusercontent.com/45923706/133132200-a4e884a2-574c-46ef-9b8a-3a48d65490ba.png)

The consumer service is a default service that takes all of your consumers and consume from Kafka, when find a new message, it'll call the HandleConsumeAsync method implemented in your Consumer.

# Producer

You need to implement this abstract class and implement the topic you want to produce, and the type of the message you want, eg. Invoice.
It can be any kind of class. It uses json serializer to serialize it to Kafka.
![image](https://user-images.githubusercontent.com/45923706/133131444-8bc7d8e7-7c6b-4b92-b704-6362985e5a07.png)

Then you need to inject the producer by giving the message you passed to it.
![image](https://user-images.githubusercontent.com/45923706/133131683-76a38c84-a102-4d9e-82aa-7b16306355f8.png)

And then you can call ProduceAsync, passing the Key and the message
![image](https://user-images.githubusercontent.com/45923706/133131733-13929125-214f-4466-a8a5-935dafdfdc91.png)


# Consumer

You need to implement the consumerBase class passing the type of the message, the topics you want and the handler, what you'll do after getting a message
![image](https://user-images.githubusercontent.com/45923706/133132452-cb32b9b2-5e9b-48b0-9328-77e8dba9cf37.png)

If you chose to use the default consumer service, it'll run automatically when you start the service, it'll start consuming.
