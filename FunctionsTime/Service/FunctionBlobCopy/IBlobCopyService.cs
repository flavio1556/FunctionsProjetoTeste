using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionsAPP.Service.FunctionCopy
{
    public interface IBlobCopyService
    {
        Task SaveBlobAsync(Stream input,string name);
    }
}
