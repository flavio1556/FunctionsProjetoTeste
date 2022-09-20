using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FunctionsTime.Entiti;
using FunctionsTime.Service;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionsTime
{
    [StorageAccount("AzureWebJobsStorage")]
    public class FunctionTime
    {
        private readonly IBlobWrite _IBlobProd;
        private readonly IConsumerService _consumer;
        public FunctionTime(IBlobWrite iblobprod, IConsumerService consumer)
        {
            _IBlobProd = iblobprod;
            _consumer = consumer;
        }
        
        [FunctionName("FunctionTime")]
        public async Task  Run([TimerTrigger("0 */9 * * * *")]TimerInfo myTimer, ILogger log)
        {
            try
            {
                var result = await _consumer.GetDataAPI();
               
                
                _IBlobProd.WriteInBlob(result);
            }catch(Exception ex)
            {
                log.LogInformation($"Erro in function, message erro: {ex.Message}");
            }
            finally
            {

                log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            }
           
        }
    }
}
