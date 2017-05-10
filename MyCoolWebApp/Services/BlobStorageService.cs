using System.Threading.Tasks;

namespace MyCoolWebApp.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        public async  Task<string> ReadBlobStorageFileAsync()
        {
            return await Task.FromResult("test");
        }
    }
}