using Azure.AI.TextAnalytics;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Stella.DEVO.Esame.Receiver
{
    public interface IReadQueue
    {
        TextAnalyticsClient Client { get; set; }

        void Run([QueueTrigger("esame", Connection = "AzureWebJobsStorage")] string myQueueItem, ILogger log);
    }
}