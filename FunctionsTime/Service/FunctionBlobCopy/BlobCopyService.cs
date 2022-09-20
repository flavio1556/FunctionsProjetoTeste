using Azure.Storage.Blobs;
using FunctionsTime.ContexBlob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FunctionsAPP.Service.FunctionCopy
{
    public class BlobCopyService : IBlobCopyService
    {
        private readonly string _nameBlob = Environment.GetEnvironmentVariable("ContainerNamebkp");
        private readonly IContextBlob _contextBlob;
        private readonly BlobContainerClient _blobContainer;
        public BlobCopyService(IContextBlob contextBlob)
        {
            _contextBlob = contextBlob;
            _blobContainer = _contextBlob.GetContainer(this._nameBlob);
        }
        public async Task SaveBlobAsync(Stream input,string name)
        {
            await _blobContainer.CreateIfNotExistsAsync();
            var blob = _blobContainer.GetBlobClient(name);
            
            using (var writer = input)
            {
                await blob.UploadAsync(writer,true);
            }
        }
    }
}
