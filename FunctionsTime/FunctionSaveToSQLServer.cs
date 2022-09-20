// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Azure.EventGrid;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using FunctionsAPP.Service.PersonService;
using FunctionsAPP.Service.FunctionReadBlob;
using FunctionsAPP.Ultis;
using System;

namespace FunctionsAPP
{

    public  class FunctionSaveToSQLServer
    {
        private readonly IPersonService _personservice;
        private readonly IBlobServiceRead _blobServiceRead;

        public FunctionSaveToSQLServer(IPersonService personservice, IBlobServiceRead blobServiceRead)
        {
            _personservice = personservice;
            _blobServiceRead = blobServiceRead;
        }

        [FunctionName("FunctionSaveToSQLServer")]
        public  async Task Run([EventGridTrigger]EventGridEvent eventGridEvent, ILogger log)
        {
            if ((object)eventGridEvent.Data is JObject jObject)
            {
                if (eventGridEvent.EventType == EventTypes.StorageBlobCreatedEvent)
                {
                    var blobCreatedEvent = jObject.ToObject<StorageBlobCreatedEventData>();
                   
                    if(blobCreatedEvent != null)
                    {
                      var resultString =  await _blobServiceRead.ReadBlobAsync(blobCreatedEvent.Url);
                      if(resultString != string.Empty)
                        {
                           await _personservice.CreateByBlob(_personservice.DeserializePerson(resultString, blobCreatedEvent.Url.GetLastStringURL()));
                        }

                    }
                    else
                    {
                        log.LogInformation("the data event invalid: " + blobCreatedEvent);
                    }
                   
                   
                }
            }
            else
            {
                log.LogInformation("the data event invalid: " +  eventGridEvent.Data.ToString());
            }
            
            log.LogInformation(eventGridEvent.Data.ToString());
        }
    }
}
