using FunctionsAPP.Entity;
using FunctionsAPP.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionsAPP.Service.PersonService
{
    public interface IPersonService
    {
        Task<Person> GetById(long Id);
        Task<PersonCompleteModels> GetblobByCPF(long cpf);
        Task<List<Person>> GetAll();
        Task<Person> Create(Person person);
        Task<bool> CreateByBlob(List<Person> people);
        Task<Person> Update(Person person);
        Task<bool> Delete(long id);
        List<Person> DeserializePerson(string json,string blobname);
        PersonCompleteModels DeserializePersonFilterCPF(string json, long cpf);
    }
}
