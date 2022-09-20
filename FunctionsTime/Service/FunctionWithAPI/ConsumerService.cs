using FunctionsAPP.Entity.Models;
using FunctionsTime.RefitAcess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionsTime.Service
{
    internal class ConsumerService : IConsumerService
    {
        private readonly IIntegrationData _repository;
        public ConsumerService([FromServices] IIntegrationData repository)
        {
            _repository = repository;
        }

        public async Task<bool> DeltePersonAPI(PersonModelDelete PersonModelDelete)
        {
            var result = await _repository.DeletePersonBase(PersonModelDelete);
            return result;
        }

        public async Task<string> GetDataAPI()
        {
            var result = await _repository.GetIntegrationDataAsync();
            return result;
        }
    }
}
