using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using Stella.DEVO.Esame.DataAccess.Models;
using System;
using System.Text.Json;

namespace Stella.DEVO.Esame.Sender
{
    public class Worker : IWorker
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _queueName;

        public Worker(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("Queue");
            _queueName = _configuration["QueueName"];
        }

        public void SendToQueue(InputText message)
        {
            QueueClient queueClient = new QueueClient(_connectionString, _queueName);

            queueClient.CreateIfNotExists();

            if (queueClient.Exists())
            {
                // Send a message to the queue
                byte[] textEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(JsonSerializer.Serialize(message));
                var text = System.Convert.ToBase64String(textEncodeAsBytes);
                queueClient.SendMessage(text);
            }
        }

    }
}
