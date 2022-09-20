using FunctionsAPP.ContexBlob;
using FunctionsAPP.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FunctionsAPP.Repository
{
    public class PersonRepositoryImplementations : RepositoryImplemantations<Person>, IPersonRepository
    {
        public PersonRepositoryImplementations(ContextSQL context) : base(context)
        {
        }

        public async Task<Person> GetByCPF(long cpf)
        {
            try
            {
                return await _dbset.FirstOrDefaultAsync(p => p.CPF == cpf);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
