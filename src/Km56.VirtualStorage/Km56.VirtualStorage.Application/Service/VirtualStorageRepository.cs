using km56.VirtualStorage.Application.Common;
using km56.VirtualStorage.Application.Dto;
using km56.VirtualStorage.Application.Validation;
using Microsoft.Extensions.Logging;

namespace km56.VirtualStorage.Application.Service
{
    /// <summary>
    /// Implements a Virtual Storage Repository.
    /// Defines the 3 operations for a Virtual Storage: Create, Download and Delete a File.
    /// It resolves what is the concrete Virtual Storage Service to be used based on a Connection Name
    /// </summary>
    public class VirtualStorageRepository : IVirtualStorageRepository
    {
        private Func<string, IVirtualStorageService> _virtualStorageService; 
        private readonly ILogger<VirtualStorageRepository> _logger;

        public VirtualStorageRepository(Func<string, IVirtualStorageService> virtualStorageService, ILogger<VirtualStorageRepository> logger) 
        { 
            _virtualStorageService = virtualStorageService;
            _logger = logger;
        }

        public async Task<CreateFileResult> CreateFile(CreateFileRequestDto createFileRequest)
        {
            try
            {
                var requestValidator = new AnnotationValidator<CreateFileRequestDto>();

                if (requestValidator.IsValid(createFileRequest)) 
                {
                    var concreteVirtualStorage = _virtualStorageService(createFileRequest.ConnectionName!);
                    var createFileResult = await concreteVirtualStorage.CreateFile(createFileRequest.FilePath!, createFileRequest.FileName!, createFileRequest.FileContent!);
                    return createFileResult;
                }
                else
                {
                    var failedResult = new CreateFileResult
                    {
                        Successful = false,
                        ErrorMessage = requestValidator.ErrorMessages
                    };

                    return failedResult;
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Create File Error");

                var failedResult = new CreateFileResult
                {
                    Successful = false,
                    ErrorMessage = $"Create File Error: {ex.Message} when using Connection Name '{createFileRequest?.ConnectionName ?? "Unknown"}'"
                };

                return failedResult;
            }
        }

        public async Task<bool> DeleteFile(DeleteFileRequestDto deleteFileRequest)
        {
            try
            {
                var requestValidator = new AnnotationValidator<DeleteFileRequestDto>();

                if (requestValidator.IsValid(deleteFileRequest))
                {
                    var concreteVirtualStorage = _virtualStorageService(deleteFileRequest.ConnectionName!);

                    if (concreteVirtualStorage == null)
                    {
                        return false;
                    }
                    else
                    {
                        var successful = await concreteVirtualStorage.DeleteFile(deleteFileRequest.FilePath!, deleteFileRequest.FileName!);
                        return successful;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "Delete File Error");

                return false;
            }
        }

        public async Task<byte[]> DownloadFile(DownloadFileRequestDto dowloadFileRequest)
        {
            try
            {
                var requestValidator = new AnnotationValidator<DownloadFileRequestDto>();

                if (requestValidator.IsValid(dowloadFileRequest))
                {
                    var concreteVirtualStorage = _virtualStorageService(dowloadFileRequest.ConnectionName!);

                    if (concreteVirtualStorage == null)
                    {
                        return new byte[] { };
                    }
                    else
                    {
                        var fileContent = await concreteVirtualStorage.DownloadFile(dowloadFileRequest.FilePath!, dowloadFileRequest.FileName!);
                        return fileContent;
                    }
                }
                else
                {
                    return new byte[] { };
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Download File Error");

                return new byte[] { };
            }
        }
    }
}
