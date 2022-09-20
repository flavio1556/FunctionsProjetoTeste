using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using FunctionsAPP.Entity.Models;
using FunctionsAPP.Service.PersonService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace FunctionsAPP
{
    public class FunctionGetObjectComplete
    {
        private readonly ILogger<FunctionGetObjectComplete> _logger;
        private readonly IPersonService _personservice;

        public FunctionGetObjectComplete(ILogger<FunctionGetObjectComplete> logger, IPersonService personservice)
        {
            _logger = logger;
            _personservice = personservice;
        }

        [FunctionName("FunctionGetObjectComplete")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "CPF" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "CPF", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **CPF** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(PersonCompleteModels), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {
            try
            {
                _logger.LogInformation("C# HTTP trigger function processed a request.");

                long CPF = long.Parse(req.Query["CPF"]);


                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                CPF = CPF != 0 ? CPF : data?.CPF;
                var result = await _personservice.GetblobByCPF(CPF);


                return new OkObjectResult(result);
            }
            catch(Exception ex)
            {
                _logger.LogInformation("Sucesso in delete Informartion Person");
                var result = new ObjectResult(ex.Message);
                result.StatusCode = StatusCodes.Status500InternalServerError;
                return result;
                
            }

        }
    }
}

