using FunctionsAPP.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionsAPP.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<Person> GetByCPF(long cpf);
    }
}
