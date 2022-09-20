using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionsTime.ContexBlob
{
     public interface IContextBlob 
    {
        BlobContainerClient GetContainer(string containerName);
    }
}
