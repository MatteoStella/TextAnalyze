using System;
using System.Text.Json;
using Azure;
using Azure.AI.TextAnalytics;
using Azure.Storage.Queues;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stella.DEVO.Esame.DataAccess.Models;
using Stella.DEVO.Esame.DataAccess.Services;

namespace Stella.DEVO.Esame.Receiver
{
    public class ReadQueue : IReadQueue
    {
        private readonly IConfiguration _configuration;
        private readonly QueueClient _queueClient;
        private readonly AzureKeyCredential _key = new AzureKeyCredential("yourKey");
        private readonly Uri _endpoint = new Uri("yourEndpoint");
        private readonly IInsertData _insertData;

        public TextAnalyticsClient Client { get; set; }

        public ReadQueue(IConfiguration configuration, IInsertData insertData)
        {
            _configuration = configuration;

            var connectionString = _configuration.GetConnectionString("Storage");

            _queueClient = new QueueClient(connectionString, "yourQueueName");
            _queueClient.CreateIfNotExists();
            _insertData = insertData;
        }

        [FunctionName("ReadQueue")]
        public void Run([QueueTrigger("yourQueueName", Connection = "AzureWebJobsStorage")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Timer trigger eseguito alle: {DateTime.Now}");

            InputText obj = new InputText();

            if (_queueClient.Exists())
            {
                log.LogInformation("Messaggio letto");

                if (myQueueItem != null)
                {
                    obj = JsonSerializer.Deserialize<InputText>(myQueueItem);
                    log.LogInformation(obj.Text);
                }
            }

            Client = new TextAnalyticsClient(_endpoint, _key);

            if (obj.Text != null)
            {
                DocumentSentiment documentSentiment = Client.AnalyzeSentiment(obj.Text);
                var model = new SqlModel
                {
                    Text = obj.Text,
                    Date = obj.Date,
                    Sentiment = documentSentiment.Sentiment.ToString(),
                    Positive = documentSentiment.ConfidenceScores.Positive.ToString(),
                    Negative = documentSentiment.ConfidenceScores.Negative.ToString(),
                    Neutral = documentSentiment.ConfidenceScores.Neutral.ToString()
                };

                _insertData.Insert(model);
            }

        }
    }
}
