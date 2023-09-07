using km56.VirtualStorage.Application.Common;
using km56.VirtualStorage.Application.Dto;

namespace km56.VirtualStorage.Application.Service
{
    public interface IVirtualStorageRepository
    {
        Task<CreateFileResult> CreateFile(CreateFileRequestDto createFileRequest);

        Task<byte[]> DownloadFile(DownloadFileRequestDto dowloadFileRequest);

        Task<bool> DeleteFile(DeleteFileRequestDto deleteFileRequest);
    }
}
