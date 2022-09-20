using Azure.Storage.Blobs;
using FunctionsTime.ContexBlob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using FunctionsAPP.Ultis;
using System.Threading.Tasks;

namespace FunctionsAPP.Service.FunctionReadBlob
{
    public class BlobReadServiceImplementations : IBlobServiceRead
    {
        private readonly string _nameContainer = Environment.GetEnvironmentVariable("ContainerNamebkp");
        private readonly IContextBlob _contextBlob;
        private readonly BlobContainerClient _blobContainer;
        private object blobCreatedEvent;

        public BlobReadServiceImplementations(IContextBlob contextBlob)
        {
            _contextBlob = contextBlob;
            _blobContainer = _contextBlob.GetContainer(this._nameContainer);
        }

        public async Task<string> ReadBlobAsync(string blobUri)
        {
            string result = string.Empty;
            string blobName = blobUri.GetLastStringURL();
             if(await _blobContainer.GetBlobClient(blobName).ExistsAsync())
            {
                var blobClient = _blobContainer.GetBlobClient(blobName);
                var response = await blobClient.DownloadAsync();
                if(response.Value.Content != null)
                {
                    result = GetStringByStream(response.Value.Content);
                }
            }
            else 
            {
                var account = new StorageCredentials(Environment.GetEnvironmentVariable("AccountName")
                        , Environment.GetEnvironmentVariable("AcessKeyAccout"));
                var blobNameCloudBlock = new CloudBlockBlob(new Uri(blobUri), account);
                if(await blobNameCloudBlock.ExistsAsync())
                {
                    var blobClient = _blobContainer.GetBlobClient(blobNameCloudBlock.Name);
                    var response = await blobClient.DownloadAsync();
                    if (response.Value.Content != null)
                    {
                        result = GetStringByStream(response.Value.Content);
                    }
                }

            }
             return result;
        }

        public async Task<string> ReadBlobByNameAsync(string blobName)  
        {
            string result = string.Empty;
            var blobClient = _blobContainer.GetBlobClient(blobName);
            var response = await blobClient.DownloadAsync();
            if (response.Value.Content != null)
            {
                result = GetStringByStream(response.Value.Content);
            }
            return result;
        }

        private string GetStringByStream(Stream stream)
        {
            var result = string.Empty;
            using (var streamReader = new StreamReader(stream))
            {
                result = streamReader.ReadToEnd();

            }
            return result;
        }
    }
}
