using Azure.Storage.Blobs;
using FunctionsTime.ContexBlob;
using FunctionsTime.Entiti;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FunctionsTime.Service
{
    internal class BlobWrite : IBlobWrite
    {
        private readonly string _nameBlob = Environment.GetEnvironmentVariable("ContainerName");
        private readonly IContextBlob _contextBlob;
        private readonly BlobContainerClient _blobContainer;
        public BlobWrite(IContextBlob contextBlob)
        {            
            _contextBlob = contextBlob;
            _blobContainer = _contextBlob.GetContainer(this._nameBlob);
        }
        public async void WriteInBlob(string jsonstring)
        {
            
            try
            {
                await _blobContainer.CreateIfNotExistsAsync();
                var blob = _blobContainer.GetBlobClient($"json{DateTime.Now.ToString("dd-MM-yyy")}.json");
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonstring)))
                {
                    await blob.UploadAsync(ms, true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
                      

        }
    }
}
