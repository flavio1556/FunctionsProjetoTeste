using FunctionsAPP;
using FunctionsAPP.ContexBlob;
using FunctionsAPP.Repository;
using FunctionsAPP.Service.Common;
using FunctionsAPP.Service.FunctionCopy;
using FunctionsAPP.Service.FunctionDeleteBlobService;
using FunctionsAPP.Service.FunctionReadBlob;
using FunctionsAPP.Service.PersonService;
using FunctionsTime.ContexBlob;
using FunctionsTime.RefitAcess;
using FunctionsTime.Service;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

[assembly: FunctionsStartup(typeof(FunctionsTime.Startup))]
namespace FunctionsTime
{

    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IContextBlob, ContextBlob>();
            builder.Services.AddSingleton<IBlobWrite, BlobWrite>();
            builder.Services.AddSingleton<IConsumerService, ConsumerService>();
            builder.Services.AddSingleton<IBlobCopyService, BlobCopyService>();
            builder.Services.AddSingleton<IBlobDeleteService, BlobDeleteServiceBlob>();
            builder.Services.AddSingleton<IBlobDeleteService, BlobDeleteServiceBlob>();
            builder.Services.AddSingleton<IBlobServiceRead, BlobReadServiceImplementations>();
            builder.Services.AddTransient<IPersonService, PersonServiceImplementations>();
            builder.Services.AddTransient<IPublishEventService, PublishEventImplementations>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(RepositoryImplemantations<>));
            builder.Services.AddScoped(typeof(IPersonRepository), typeof(PersonRepositoryImplementations));
            builder.Services.AddDbContext<ContextSQL>(options =>
            {
                options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionSql"));
            });
            var baseurl = Environment.GetEnvironmentVariable("baseurl");

            
            
            builder.Services.AddRefitClient<IIntegrationData>()
                .ConfigureHttpClient(
                    c => { 
                        c.BaseAddress = new Uri(baseurl);
                        c.DefaultRequestHeaders
                          .Accept
                          .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    });
        }
    }
}
