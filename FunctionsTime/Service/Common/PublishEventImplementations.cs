using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionsAPP.Service.Common
{
    public class PublishEventImplementations : IPublishEventService
    {
        public async Task<bool> PublishEvent(string domainEndPoint, string domainKey,IList<EventGridEvent> eventGridEvent)
        {
            string domainHostname = new Uri(domainEndPoint).Host;
            TopicCredentials domainKeyCredentials = new TopicCredentials(domainKey);
            EventGridClient client = new EventGridClient(domainKeyCredentials);

            await client.PublishEventsAsync(domainHostname, eventGridEvent);
            Console.Write("Published events to Event Grid domain.");
            return true;
        }
    }
}
