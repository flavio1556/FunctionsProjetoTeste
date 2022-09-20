// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Azure.Messaging.EventGrid;
using FunctionsTime.Service;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using FunctionsAPP.Entity.Models;
using Newtonsoft.Json;

namespace FunctionsAPP
{
    public  class FunctionReportSuccess
    {
        private readonly IConsumerService _consumer;

        public FunctionReportSuccess(IConsumerService consumer)
        {
            _consumer = consumer;
        }

        [FunctionName("FunctionReportSuccess")]
        public async Task Run([EventGridTrigger]EventGridEvent eventGridEvent, ILogger log)
        {
           
                if (eventGridEvent.EventType == "PersonInserted")
                {
                    try
                    {
                        var personModelDelete = JsonConvert.DeserializeObject<PersonModelDelete>(eventGridEvent.Data.ToString());
                        //var personModelDelete = new PersonModelDelete();
                        await _consumer.DeltePersonAPI(personModelDelete);
                        
                    }catch(Exception ex){
                        log.LogInformation("Erro in delete Person Information, erro:" +ex.Message);
                    }
                    finally
                    {
                        log.LogInformation("Sucesso in delete Informartion Person");
                    }
                   
                }
            
            log.LogInformation(eventGridEvent.Data.ToString());
        }
    }
}
