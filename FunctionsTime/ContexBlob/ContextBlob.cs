using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionsTime.ContexBlob
{
    internal class ContextBlob : IContextBlob
    {
        private  readonly string _connectionString;
        public ContextBlob()
        {
            _connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
        }
        public BlobContainerClient GetContainer(string containerName)
        {
            return new BlobContainerClient(_connectionString, containerName);
        }
    }
}
