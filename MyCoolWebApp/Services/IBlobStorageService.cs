using System.Threading.Tasks;

namespace MyCoolWebApp.Services
{
    public interface IBlobStorageService
    {
        Task<string> ReadBlobStorageFileAsync();
    }
}
