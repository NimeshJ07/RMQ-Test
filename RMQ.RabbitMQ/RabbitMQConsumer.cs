using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

public class RabbitMQConsumer
{
    public void Consume()
    {
        try
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            Console.WriteLine("Connection Created!!");
            using (var connection = factory.CreateConnection())

            using (var channel = connection.CreateModel())
            {
                // Declare queue to ensure it exists
                channel.QueueDeclare(queue: "product",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: true,
                                     arguments: null);
                Console.WriteLine("Queue declared.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                };

                channel.BasicConsume(queue: "product",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine("Consuming started. Press [enter] to exit.");
                Console.ReadLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
}
