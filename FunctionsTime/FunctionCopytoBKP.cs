using System;
using System.IO;
using System.Threading.Tasks;
using FunctionsAPP.Service.FunctionCopy;
using FunctionsAPP.Service.FunctionDeleteBlobService;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionsAPP
{
    public class FunctionCopytoBKP
    {
        private readonly IBlobCopyService _service;
        private readonly IBlobDeleteService _blobDeleteService;

        public FunctionCopytoBKP(IBlobCopyService service, IBlobDeleteService blobDeleteService)
        {
            _service = service;
            _blobDeleteService = blobDeleteService;
        }

        [StorageAccount("AzureWebJobsStorage")]
        [FunctionName("FunctionCopytoBKP")]
        public async Task Run([BlobTrigger("prod/{name}")]Stream myBlob, string name, ILogger log)
        {
            try
            {
                await _service.SaveBlobAsync(myBlob,$"{name}");
                await _blobDeleteService.DeleteBlobAsync(name);
            }catch(Exception ex)
            {
                log.LogInformation($"Blob trigger function Failed erro:{ex.Message}");
            }
            finally
            {
                log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            }
            
        }
    }
}
