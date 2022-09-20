using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionsAPP.Service.FunctionDeleteBlobService
{
    public interface IBlobDeleteService
    {
        public Task DeleteBlobAsync(string blobName);
    }
}
