using FunctionsAPP.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionsTime.Service
{
    public interface IConsumerService
    {
       Task<string> GetDataAPI();
        Task<bool> DeltePersonAPI(PersonModelDelete PersonModelDelete);
    }
}
