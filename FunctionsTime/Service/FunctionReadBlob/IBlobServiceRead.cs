using System.Threading.Tasks;

namespace FunctionsAPP.Service.FunctionReadBlob
{
    public interface IBlobServiceRead
    {

        
        Task<string> ReadBlobAsync(string blobUri);
        Task<string> ReadBlobByNameAsync(string blobUri);
    }
}
