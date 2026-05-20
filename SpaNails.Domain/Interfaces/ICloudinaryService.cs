using System.IO;
using System.Threading.Tasks;

namespace SpaNails.Domain.Interfaces
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(Stream imageStream, string fileName);
        Task<bool> DeleteImageAsync(string publicId);
    }
}
