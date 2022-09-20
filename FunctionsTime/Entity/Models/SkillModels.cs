using FunctionsAPP.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionsAPP.Entity.Models
{
    public class SkillModels
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public EnumCommon.TypeSkill TypeSkill { get; set; }
        public EnumCommon.NivelSkill NivelSkill { get; set; }
    }
}
