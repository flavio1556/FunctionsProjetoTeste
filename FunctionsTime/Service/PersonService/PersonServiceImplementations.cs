using System;
using FunctionsAPP.Service.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FunctionsAPP.Entity;
using FunctionsAPP.Repository;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Newtonsoft.Json;
using FunctionsAPP.Service.FunctionReadBlob;
using FunctionsAPP.Entity.Models;

namespace FunctionsAPP.Service.PersonService
{
    internal class PersonServiceImplementations : IPersonService
    {
        private readonly string _topicEventPerson = Environment.GetEnvironmentVariable("TopicEventPerson");
        private readonly string _subscription = Environment.GetEnvironmentVariable("Subscription");
        private readonly string _resourceGroupEventPerson = Environment.GetEnvironmentVariable("ResourceGroupEventPerson");
        private readonly string _domainEndPointTopicPerson = Environment.GetEnvironmentVariable("domainEndPointTopicPerson");
        private readonly string _domainKeyTopicPerson = Environment.GetEnvironmentVariable("domainKeyTopicPerson");
        private readonly IPersonRepository _repository;
        private readonly IBlobServiceRead _blobServiceRead;
        private IList<EventGridEvent> ListEventPerson { get; set; } = new List<EventGridEvent>();
        private readonly IPublishEventService _publishEventService;
        public PersonServiceImplementations(IPersonRepository repository, IPublishEventService publishEventService, IBlobServiceRead blobServiceRead)
        {
            _repository = repository;
            _publishEventService = publishEventService;
            _blobServiceRead = blobServiceRead;
        }

        public async Task<List<Person>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Person> GetById(long Id)
        {
            return await _repository.Get(Id);
        }
        public async Task<PersonCompleteModels> GetblobByCPF(long cpf)
        {

            try
            {
                var result = await _repository.GetByCPF(cpf);
                if (result == null) return null;
                string blobvalue = await _blobServiceRead.ReadBlobAsync(result.BlobName);
                return this.DeserializePersonFilterCPF(blobvalue, cpf);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<Person> Create(Person person)
        {
            return await _repository.Create(person);
        }

        public async Task<Person> Update(Person person)
        {
            return await _repository.Update(person);
        }

        public async Task<bool> CreateByBlob(List<Person> people)
        {
            try
            {
                foreach (Person person in people)
                {
                    var result = await this.Create(person);
                    if (result != null)
                    {
                        var exist = await _repository.Exist(result.Id);
                        if (exist)
                        {
                            ListEventPerson.Add(GetEventPerson(result));
                        }
                    }
                }
                await _publishEventService.PublishEvent(_domainEndPointTopicPerson, _domainKeyTopicPerson, ListEventPerson);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }
        public async Task<bool> Delete(long id)
        {
            return await _repository.Delete(id);
        }
        public List<Person> DeserializePerson(string json, string blobname)
        {
            List<Person> result;
            result = JsonConvert.DeserializeObject<List<Person>>(json);
            result.ToList().ForEach(p => p.BlobName = blobname);
            return result;
        }

        public PersonCompleteModels DeserializePersonFilterCPF(string json, long cpf)
        {

            var result = JsonConvert.DeserializeObject<List<PersonCompleteModels>>(json).SingleOrDefault(p => p.CPF == cpf);
            return result;
        }

        private EventGridEvent GetEventPerson(Person person)
        {
            EventGridEvent result = new EventGridEvent()
            {
                Id = Guid.NewGuid().ToString(),
                EventType = "PersonInserted",
                Topic = $"/subscriptions/{_subscription}/resourceGroups/{_resourceGroupEventPerson}/providers/Microsoft.EventGrid/topics/{_topicEventPerson}",
                Data = person,
                EventTime = DateTime.Now,
                Subject = $"sql/person_blob",
                DataVersion = "2.0"
            };
            return result;

        }
    }
}
