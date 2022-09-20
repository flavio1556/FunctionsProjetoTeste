using FunctionsAPP.Entity.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace FunctionsTime.RefitAcess
{
    internal interface IIntegrationData
    {
        [Get("/api/People")]
        Task<string> GetIntegrationDataAsync();

        [Delete("/api/People/DeleteByObject")]
        [Headers("Content-Type:application/json")]
        Task<bool> DeletePersonBase ([Body] PersonModelDelete personModelDelete);
    }
}
