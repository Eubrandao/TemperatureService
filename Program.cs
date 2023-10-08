using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace temperature
{
    class Program
    {
        const string ServiceBusConnectionString = "yourConnectionString";
        const string TopicName = "temperature";

        static void Main(string[] args)
        {
            Console.WriteLine("Sending a message to the temperature  topic...");
            sendInfoTemperature().GetAwaiter().GetResult();
            Console.WriteLine("Message was sent successfully.");
        }

        static async Task sendInfoTemperature()
        {
      
            await using var client = new ServiceBusClient(ServiceBusConnectionString);

            await using ServiceBusSender sender = client.CreateSender(TopicName);

            try
            {
                string messageBody = "30";
                var message = new ServiceBusMessage(messageBody);
                Console.WriteLine($"Sending message: {messageBody}");
                await sender.SendMessageAsync(message);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }
    }
}