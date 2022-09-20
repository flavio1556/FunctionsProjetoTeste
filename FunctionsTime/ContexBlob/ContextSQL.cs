using FunctionsAPP.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionsAPP.ContexBlob
{
    public class ContextSQL : DbContext
    {
        public ContextSQL(DbContextOptions<ContextSQL> options) :base(options)
        {
        }
        DbSet<Person> People { get; set; }
    }
}
