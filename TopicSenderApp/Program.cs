using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;


namespace TopicSenderApp
{
    class Program
    {

        const string ServiceBusConnectionString = "Endpoint=sb://servicebuspractice.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=44Xtdc5DTF2dnGjGiRYnYAVfyPVCW3ZVM6boz8Ty/gE=";
        const string TopicName = "practicetopic";
        static ITopicClient topicClient;

        public static async Task Main(string[] args)
        {
            const int NumberOfMessages = 4;

            topicClient = new TopicClient(ServiceBusConnectionString, TopicName);

            Console.WriteLine("Press key when all messages are sent");

            await SendMessageAsync(NumberOfMessages);
            Console.ReadKey();

            await topicClient.CloseAsync();

        }

        static async Task SendMessageAsync( int numberOfMessages)
        {
            try
            {
                for (var i = 0; i < numberOfMessages; i++)
                {
                    string messageBody = $"Message number {i}";

                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                    Console.WriteLine($"Sending message: {messageBody}");

                    await topicClient.SendAsync(message);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: { e.Message}");
            }
        }
    }
}
