using Amazon.Runtime.Internal.Util;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using km56.VirtualStorage.Application.Common;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace km56.VirtualStorage.Application.Service
{
    /// <summary>
    /// Virtual Storage Service for Azure Cloud Storage Service
    /// </summary>
    public class AzureCloudStorageService : IVirtualStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public AzureCloudStorageService(IConfiguration configuration) 
        {
            string connectionString = configuration.GetConnectionString(VirtualStorageIdentifier.Azure);
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<CreateFileResult> CreateFile(string filePath, string fileName, byte[] fileContent)
        {
            using (var stream = new MemoryStream(fileContent))
            {
                BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(filePath);
                BlobClient blobClient = containerClient.GetBlobClient(fileName);

                await blobClient.UploadAsync(stream, true);

                CreateFileResult result = new CreateFileResult
                {
                    Successful = true
                };

                return result;
            }
        }

        public async Task<byte[]> DownloadFile(string filePath, string fileName)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(filePath);
            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            BlobDownloadInfo downloadInfo = await blobClient.DownloadAsync();

            MemoryStream memoryStream = new MemoryStream();
            await downloadInfo.Content.CopyToAsync(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream.ToArray();
        }

        public async Task<bool> DeleteFile(string filePath, string fileName)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(filePath);
            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            var response = await blobClient.DeleteAsync();

            return !response.IsError;
        }
    }
}
