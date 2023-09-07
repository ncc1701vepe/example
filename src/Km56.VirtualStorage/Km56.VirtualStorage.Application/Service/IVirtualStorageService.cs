using km56.VirtualStorage.Application.Common;

namespace km56.VirtualStorage.Application.Service
{
    /// <summary>
    /// Defines the contract for a Virtual Storage Repository.
    /// This is the service definition that would be exposed to the API 
    /// </summary>
    public interface IVirtualStorageService
    {
        Task<CreateFileResult> CreateFile(string filePath, string fileName, byte[] fileContent);

        Task<byte[]> DownloadFile(string filePath, string fileName);

        Task<bool> DeleteFile(string filePath, string fileName);
    }
}
