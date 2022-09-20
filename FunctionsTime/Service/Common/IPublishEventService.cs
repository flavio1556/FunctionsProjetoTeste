using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FunctionsAPP.Service.Common
{
    public interface  IPublishEventService
    {
        Task<bool> PublishEvent(string domainEndPoint, string domainKey, IList<EventGridEvent> eventGridEvent);
    }
}
