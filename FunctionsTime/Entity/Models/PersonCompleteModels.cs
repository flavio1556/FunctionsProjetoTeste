using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionsAPP.Entity.Models
{
    public class PersonCompleteModels
    {
        public long Id { get; set; }
        public long CPF { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public List<SkillModels> Skills { get; set; }
    }
}
