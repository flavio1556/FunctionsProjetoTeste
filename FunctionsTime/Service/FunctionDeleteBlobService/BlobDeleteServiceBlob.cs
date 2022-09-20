using Azure.Storage.Blobs;
using FunctionsTime.ContexBlob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionsAPP.Service.FunctionDeleteBlobService
{
    internal class BlobDeleteServiceBlob : IBlobDeleteService
    {
        private readonly string _nameBlob = Environment.GetEnvironmentVariable("ContainerName");
        private readonly IContextBlob _contextBlob;
        private readonly BlobContainerClient _blobContainer;
        public BlobDeleteServiceBlob(IContextBlob contextBlob)
        {
            _contextBlob = contextBlob;
            _blobContainer = _contextBlob.GetContainer(this._nameBlob);
        }

        public async Task DeleteBlobAsync(string blobName)
        {
          await _blobContainer.GetBlobClient(blobName).DeleteAsync();
        }
    }
}
